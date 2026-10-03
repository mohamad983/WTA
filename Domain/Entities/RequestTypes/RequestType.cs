using Domain.Common;
using Domain.Entities.Requests;
using Domain.Entities.RequestTypes.Args;
using Domain.Entities.RequestTypes.Enums;
using Domain.Entities.RequestTypes.Events;
using Domain.Entities.WorkFlowActions;
using Domain.Entities.WorkFlowSteps;
using Domain.Entities.WorkFlowSteps.Args;
using Domain.Entities.WorkFlowSteps.Enums;
using Domain.Entities.WorkFlowStepTransitions;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Domain.Entities.RequestTypes
{
    public class RequestType : BaseEntity
    {
        //public Guid RequestTypeGuid { get; private set; } = new Guid();
        public RequestTypeStatus Status { get; private set; } = RequestTypeStatus.Draft;
        public string Title { get; private set; } = string.Empty;
        private readonly List<WorkFlowStep> _steps = [];
        public IReadOnlyCollection<WorkFlowStep> Steps => _steps.AsReadOnly();
        private readonly List<WorkFlowAction> _actions = [];
        public IReadOnlyCollection<WorkFlowAction> Actions => _actions.AsReadOnly();
        public int Code { get; private set; }
        public string? Description { get; private set; } = string.Empty;
        private RequestType()
        {

        }
        public RequestType(RequestTypeArgs args)
        { 
            if (string.IsNullOrWhiteSpace(args.Title))
            {
                throw new DomainException("The title is invalid!");
            }
            if (args.Code <= 0)
            {
                throw new DomainException("Code must be positive!");
            }
            Title = args.Title.Trim();
            Description = args.Description?.Trim() ?? string.Empty;
            Code = args.Code;
            AddDomainEvent(new RequestTypeCreated(Id, Title));
            foreach(var action in SystemActions.All)
            {
                AddActionCore(new WorkFlowAction(Id, action.Code, action.Title, isSystem: true));
            }
        }
        public void Activate()
        {
            if (Status == RequestTypeStatus.Active) return;

            var problems = GetDefinitionProblems();
            if (problems.Count > 0)
            {
                throw new DomainException($"The request type cant be activated: " + string.Join("\n, ",problems));
            }
            Status = RequestTypeStatus.Active;
            AddDomainEvent(new RequestTypeActivated(Id));
        }
        public void Deactivate()
        {
            if (Status == RequestTypeStatus.Draft) return;
            Status = RequestTypeStatus.Draft;
            AddDomainEvent(new RequestTypeDeactivated(Id));
        }
        public void Modify(RequestTypeArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Title))
            {
                throw new DomainException("The title is invalid!");
            }
            var oldTitle = Title;

            Title = args.Title.Trim();
            Description = args.Description?.Trim() ?? string.Empty;

            if(!string.Equals(Title, oldTitle, StringComparison.OrdinalIgnoreCase))
            {
                AddDomainEvent(new RequestTypeRenamed(Id, Title));
            }
        }

        public override void MarkAsDeleted(string? deletedBy = null)
        {
            if (IsDeleted) return;
            base.MarkAsDeleted(deletedBy);
            AddDomainEvent(new RequestTypeDeleted(Id));
        }

        public void AddAction(WorkFlowAction action)
        {
            EnsureDraft();
            AddActionCore(action);
        }
        public void RenameAction(Guid actionId, string title)
        {
            if (actionId == Guid.Empty)
            {
                throw new DomainException("The action Id is invalid!");
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new DomainException("Title is invalid!");
            }
            FindActiveAction(actionId).Rename(title.Trim());
            AddDomainEvent(new WorkFlowActionRenamed(Id,actionId,title));
        }
        public void RemoveAction(Guid workflowActionId)
        {
            EnsureDraft();
            var action = FindActiveAction(workflowActionId);

            if (action.IsSystem)
            {
                throw new InvalidOperationException("System Action Cant be removed");
            }
            var usedByTransition = _steps
            .Where(s => !s.IsDeleted)
            .SelectMany(s => s.transitions)
            .Any(t => !t.IsDeleted && t.ActionId == workflowActionId);

            if (usedByTransition)
                throw new InvalidOperationException("This action is used by a transition and can't be removed.");

            action.MarkAsDeleted();
            AddDomainEvent(new WorkFlowActionRemoved(Id, workflowActionId));
        }
         
        public void AddStep(WorkFlowStep step)
        {
            EnsureDraft();
            AddStepCore(step);
        }
        public void ModifyStep(Guid StepId, WorkFlowStepArgs args)
        {
            ArgumentNullException.ThrowIfNull(args);
            if (StepId == Guid.Empty)
            {
                throw new DomainException("Step Id is invalid!");
            }
            FindActiveStep(StepId).Modify(args);
        }
        public void RemoveStep(Guid StepId)
        {
            EnsureDraft();
            var step = FindActiveStep(StepId);
            var steptransitions = _steps.Where(s => !s.IsDeleted)
                .SelectMany(s => s.transitions)
                .Where(t => !t.IsDeleted && (t.CurrentStepId == StepId || t.NextStepId == StepId))
                .ToList();
            foreach (var transition in steptransitions)
            {
                transition.MarkAsDeleted();
            }
            step.MarkAsDeleted();
        }
        public void AddTransition(WorkFlowStepTransition transition)
        {
            EnsureDraft();
            if (transition.CurrentStepId == transition.NextStepId)
            {
                throw new ArgumentException("This transition has the same start and end point!");
            }
            var currentStep = FindActiveStep(transition.CurrentStepId);
            var action = FindActiveAction(transition.ActionId);
            var nextStep = FindActiveStep(transition.NextStepId);
            if (currentStep.Kind == StepKind.End)
            {
                throw new DomainException("You cannot add a transition to and end state!");
            }
            if (nextStep.Kind == StepKind.Start)
            {
                throw new DomainException("You cannot transition to a start state!");
            }
            if (!SystemActions.CanBeUsedInTransition(action.Code))
            {
                throw new DomainException("This action cannot be used in transition!");
            }
            if (currentStep.transitions.Any(t => !t.IsDeleted && 
            (t.Id == transition.Id || (t.CurrentStepId == transition.CurrentStepId &&
            t.NextStepId == transition.NextStepId && t.ActionId == transition.ActionId))))
            {
                throw new InvalidOperationException("This transition already exists!");
            }
            currentStep.AddTransition(transition);
        }
        public void RemoveTransition(Guid stepId,Guid transitionId)
        {
            EnsureDraft();
            if (stepId == Guid.Empty)
            {
                throw new DomainException("Step Id cannot be empty!");
            }
            if (transitionId == Guid.Empty)
            {
                throw new DomainException("Transition Id cannot be empty!");
            }
            FindActiveStep(stepId).RemoveTransition(transitionId);
        }

        public IReadOnlyList<string> GetDefinitionProblems()
        {
            var problems = new List<string>();
            var steps = _steps.Where(s => !s.IsDeleted).ToList();
            var transitions = steps.SelectMany(s => s.transitions).Where(t => !t.IsDeleted).ToList();
            var start = steps.SingleOrDefault(s => s.Kind == StepKind.Start);
            if (!steps.Any(s => s.Kind == StepKind.Start))
            {
                problems.Add("The workflow has no start step.");
            }
            if (!steps.Any(s => s.Kind == StepKind.End))
            {
                problems.Add("The workflow has no end step.");
            }

            foreach (var step in steps.Where(s => s.Kind != StepKind.End))
            {
                if (step.transitions.Where(t => !t.IsDeleted).ToList().Count == 0)
                {
                    problems.Add($"Step '{step.Title}' has no outgoing transition.");
                }
            }
            if (start is not null)
            {
                var reached = new HashSet<Guid>() { start.Id};
                var states = new Queue<Guid>();
                states.Enqueue(start.Id);
                while (states.Count > 0)
                {
                    var currentstate = states.Dequeue();
                    var eligble = transitions.Where(t => t.CurrentStepId == currentstate).Select(t => t.NextStepId).ToList();
                    foreach (var step in eligble)
                    {
                        if(!reached.Contains(step))
                        {
                            states.Enqueue(step);
                        }
                    }
                    reached.Add(currentstate);
                }
                foreach(var step in steps.Where(s => !reached.Contains(s.Id)))
                {
                    problems.Add($"Step '{step.Title}' can't be reached from the start step.");
                }
            }
            return problems;
                
        }
        
        private WorkFlowAction FindActiveAction(Guid actionId)
        {
            var result = _actions.SingleOrDefault(a => a.Id == actionId);
            return result ?? throw new InvalidOperationException("Action not found!");
        }
        private void AddActionCore(WorkFlowAction action)
        {
            if (action.RequestTypeId != Id)
            {
                throw new InvalidOperationException("This action belongs to a different request type!");
            }
            if (action.IsDeleted)
            {
                throw new InvalidOperationException("This action has been deleted!");
            }
            if (_actions.Any(x => !x.IsDeleted && 
            (x.Id == action.Id || string.Equals(x.Code,action.Code,StringComparison.OrdinalIgnoreCase))))
            {
                throw new InvalidOperationException("This action already exists for this request type!");
            }
            _actions.Add(action);
            AddDomainEvent(new WorkFlowActionAdded(Id, Title, action.Id, action.Code, action.Title));
        }
        private WorkFlowStep FindActiveStep(Guid StepId)
        {
            if (StepId == Guid.Empty)
            {
                throw new ArgumentException("StepId cannot be null!");
            }
            if (!_steps.Any(x => !x.IsDeleted && x.Id == StepId))
            {
                throw new InvalidOperationException("This step does not exist!");
            }
            var result = _steps.SingleOrDefault(x => x.Id == StepId);
            return result ?? throw new InvalidOperationException("Step no found!");
        }
        private void AddStepCore(WorkFlowStep step)
        {
            if (step.RequestTypeId != Id)
            {
                throw new InvalidOperationException("This step already belongs to another request type!");
            }
            if (step.IsDeleted)
            {
                throw new InvalidOperationException("This step has already been deleted!");
            }
            if (_steps.Any(x => !x.IsDeleted && x.Id == step.Id))
            {
                throw new InvalidOperationException("This step already exists!");
            }
            if (step.Kind == StepKind.Start && _steps.Any(s => !s.IsDeleted && s.Kind == StepKind.Start))
            {
                throw new DomainException("A start step already exists!");
            }
            _steps.Add(step);
        }

        private void EnsureDraft()
        {
            if (Status != RequestTypeStatus.Draft)
            {
                throw new DomainException("the WorkFlow Should be in draft to change!");
            }
        }
    }
}

using Domain.Common;
using Domain.Entities.Requests;
using Domain.Entities.RequestTypes.Args;
using Domain.Entities.WorkFlowActions;
using Domain.Entities.WorkFlowSteps;
using Domain.Entities.WorkFlowStepTransitions;
using System.Diagnostics;

namespace Domain.Entities.RequestTypes
{
    public class RequestType : BaseEntity
    {
        //public Guid RequestTypeGuid { get; private set; } = new Guid();
        public string Title { get; private set; } = string.Empty;
        private readonly List<WorkFlowStep> _steps = [];
        public IReadOnlyCollection<WorkFlowStep> Steps => _steps.AsReadOnly();
        private readonly List<WorkFlowAction> _actions = [];
        public IReadOnlyCollection<WorkFlowAction> Actions => _actions.AsReadOnly();
        private readonly List<Request> _request = [];
        public IReadOnlyCollection<Request> Requests => _request.AsReadOnly();
        public int Code { get; private set; }
        public string Description { get; private set; } = string.Empty;
        private RequestType()
        {

        }
        public RequestType(RequestTypeArgs args)
        { 
            Title = args.Title;
            Description = args.Description;
            Code = args.Code;
            foreach ( var (code,title) in SystemActions.All)
            {
                AddActionCore(new WorkFlowAction(Id, code, title, isSystem: true));
            }

        }
        public void AddRequest(Request request)
        {
            _request.Add(request);
            SetModified();
        }
        public void Modify(RequestTypeArgs args)
        {
            
            Title = args.Title;
            Description = args.Description;
            SetModified();
        }

        public void AddAction(WorkFlowAction action)
        {
            AddActionCore(action);
        }
        public void RemoveAction(Guid workflowActionId)
        {
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
        }
         
        public void AddStep(WorkFlowStep step)
        {
            AddStepCore(step);
        }
        public void RemoveStep(Guid StepId)
        {
            var step = FindActiveStep(StepId);
            var validtransitions = _steps.Where(s => !s.IsDeleted)
                .SelectMany (s => s.transitions)
                .Where(t => !t.IsDeleted && (t.NextStepId == StepId || t.CurrentStepId == StepId))
                .ToList();
            foreach(var transition in validtransitions)
            {
                transition.MarkAsDeleted();
            }
            step.MarkAsDeleted();
        }
        public void AddTransition(WorkFlowStepTransition transition)
        {
            if (transition.CurrentStepId == transition.NextStepId)
            {
                throw new ArgumentException("This transition has the same start and end point!");
            }
            var currentStep = FindActiveStep(transition.CurrentStepId);
            FindActiveAction(transition.ActionId);
            FindActiveStep(transition.NextStepId);
            if (currentStep.transitions.Any(t => !t.IsDeleted && 
            (t.Id == transition.Id || (t.CurrentStepId == transition.CurrentStepId &&
            t.NextStepId == transition.NextStepId && t.ActionId == transition.ActionId))))
            {
                throw new InvalidOperationException("This transition already exists!");
            }
            currentStep.AddTransition(transition);
        }
        public void RemoveTransition(Guid transitionId)
        {
           if (transitionId == Guid.Empty)
            {
                throw new ArgumentException("transition Id cannot be null!");
            }
           var alltransitions = _steps.
                Where(s => !s.IsDeleted)
                .SelectMany(s => s.transitions)
                .ToList();
            var transition = alltransitions.FirstOrDefault(t => t.Id == transitionId) ?? throw new InvalidOperationException("This transition does not exist!");
            if (alltransitions.Where(t => t.NextStepId == transition.NextStepId).All(t => t.Id == transitionId))
            {
                throw new InvalidOperationException("This is the only transition to taget step!");
            }
            transition.MarkAsDeleted();
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
                throw new InvalidOperationException();
            }
            _steps.Add(step);
        }
    }
}

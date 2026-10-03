using Domain.Common;
using Domain.Entities.RequestTypes;
using Domain.Entities.StepFunctions;
using Domain.Entities.Users;
using Domain.Entities.WorkFlowSteps.Args;
using Domain.Entities.WorkFlowSteps.Enums;
using Domain.Entities.WorkFlowStepTransitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Domain.Entities.WorkFlowSteps
{
    public class WorkFlowStep : BaseEntity
    {
        //public Guid WorkFlowStepId { get; private set; } = new Guid();
        public Guid RequestTypeId { get; private set; }
        public RequestType RequestType { get; private set; } = null!;
        public string Title { get; private set; } = string.Empty;
        public int StepOrder { get; private set; } = 1;
        public StepKind Kind { get; private set; }
        public Guid? ApproverUserId { get; private set; }
        public User? ApproverUser { get; private set; }
        public Guid? ApproverRoleId { get; private set; }
        public Role? ApproverRole { get; private set; } = null!;

        private readonly List<WorkFlowStepTransition> _transitions = [];
        public IReadOnlyCollection<WorkFlowStepTransition> transitions => _transitions.AsReadOnly();
        private readonly List<StepFunction> _functions = [];
        public IReadOnlyCollection<StepFunction> Functions => _functions.AsReadOnly();
        private WorkFlowStep()
        {

        }
        public WorkFlowStep(WorkFlowStepArgs args)
        {
            Apply(args);
        }
        internal void Modify(WorkFlowStepArgs args)
        {
            Apply(args);
        }
        internal void AddTransition(WorkFlowStepTransition transition)
        {
            AddTransitionCore(transition);
        }
        private void AddTransitionCore(WorkFlowStepTransition transition)
        {
            if (transition.CurrentStepId != Id)
            {
                throw new InvalidOperationException("This transition does not belong to this step!");
            }
            if (transition.IsDeleted)
            {
                throw new InvalidOperationException("This transition has already been deleted!");
            }
            _transitions.Add(transition);
        }
        internal void RemoveTransition(Guid TransitionId)
        {
            var transition = _transitions.Where(t => t.Id == TransitionId).SingleOrDefault() 
                ?? throw new DomainException("Transition was not found!");
            transition.MarkAsDeleted();
        }
        private void Apply(WorkFlowStepArgs args)
        {
            if (args.RequestTypeId == Guid.Empty)
            {
                throw new DomainException("Request type Id is invalid");
            }
            if (string.IsNullOrWhiteSpace(args.Title))
            {
                throw new DomainException("The title is invalid");
            }
            if (args.StepOrder < 1)
            {
                throw new DomainException("The step order is invalid");
            }
            if (Enum.IsDefined<StepKind>(args.kind))
            {
                throw new DomainException("The enum is not within valid values");
            }
            if (args.kind == StepKind.Approval)
            {
                if (args.ApproverRoleId == Guid.Empty || args.ApproverRoleId == null)
                {
                    throw new DomainException("Approver role Id is null or empty");
                }
                if (args.ApproverUserId == Guid.Empty)
                {
                    throw new DomainException("User Id is empty");
                }
            }
            else if (args.ApproverRoleId is not null || args.ApproverUserId is not null)
            {
                throw new DomainException("There can be no type of role or user for this step!");
            }
            RequestTypeId = args.RequestTypeId;
            Title = args.Title.Trim();
            Kind = args.kind;
            StepOrder = args.StepOrder;
            ApproverRoleId = args.ApproverRoleId;
            ApproverUserId = args.ApproverUserId;
        }
    }
}

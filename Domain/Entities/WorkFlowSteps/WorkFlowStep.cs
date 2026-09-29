using Domain.Common;
using Domain.Entities.RequestTypes;
using Domain.Entities.Users;
using Domain.Entities.WorkFlowSteps.Args;
using Domain.Entities.WorkFlowStepTransitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.WorkFlowSteps
{
    public class WorkFlowStep : BaseEntity
    {
        public Guid WorkFlowStepId { get; private set; } = new Guid();
        public Guid RequestTypeId { get; private set; }
        public RequestType RequestType { get; private set; } = null!;
        public string Title { get; private set; } = string.Empty;
        public int StepOrder { get; private set; } = 1;
        public Guid? ApproverUserId { get; private set; }
        public User? ApproverUser { get; private set; }
        public Guid ApproverRoleId { get; private set; }
        public Role ApproverRole { get; private set; } = null!;
        private readonly List<WorkFlowStepTransition>
        private WorkFlowStep()
        {

        }
        public WorkFlowStep(WorkFlowStepArgs args)
        {
            RequestTypeId = args.RequestTypeId;
            Title = args.Title;
            StepOrder = args.stepOrder;
            ApproverUserId = args.ApproverUserId;
            ApproverRoleId = args.ApproverRoleId;
        }
        public void Modify(WorkFlowStepArgs args)
        {
            Title = args.Title;
            StepOrder = args.stepOrder;
            ApproverUserId = args.ApproverUserId;
            ApproverRoleId = args.ApproverRoleId;
            SetModified();
        }
        public void ModifyTitle(string Title)
        {
            this.Title = Title;
            SetModified();
        }
        public void ModifyStepOrder (int stepOrder)
        {
            this.StepOrder = stepOrder;
            SetModified();
        }
    }
}

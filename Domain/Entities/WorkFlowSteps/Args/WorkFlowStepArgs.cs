using Domain.Entities.WorkFlowSteps.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.WorkFlowSteps.Args
{
    public class WorkFlowStepArgs
    {
        public Guid RequestTypeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int StepOrder { get; init; } = 1;
        public StepKind kind {  get; init; }
        public Guid? ApproverUserId { get; set; }
        public Guid? ApproverRoleId { get; set; }
    }
}

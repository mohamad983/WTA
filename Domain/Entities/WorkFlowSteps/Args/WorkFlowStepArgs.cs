using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.WorkFlowSteps.Args
{
    public class WorkFlowStepArgs
    {
        public Guid RequestTypeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int stepOrder { get; set; } = 1;
        public Guid? ApproverUserId { get; set; }
        public Guid ApproverRoleId { get; set; }
    }
}

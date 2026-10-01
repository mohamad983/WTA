using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.WorkFlowStepTransitions.Args
{
    public class WorkFlowStepTransitionArgs
    {
        public Guid NextStepId {  get; set; }
        public Guid CurrentStepId { get; set; }
        public Guid ActionId { get; set; }
    }
}

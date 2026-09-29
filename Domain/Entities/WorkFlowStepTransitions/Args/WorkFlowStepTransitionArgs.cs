using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.WorkFlowStepTransitions.Args
{
    public class WorkFlowStepTransitionArgs
    {
        public Guid NextStep {  get; set; }
        public Guid CurrentStep { get; set; }
    }
}

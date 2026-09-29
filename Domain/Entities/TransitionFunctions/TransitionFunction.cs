using Domain.Entities.Functions;
using Domain.Entities.WorkFlowStepTransitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.TransitionFunctions
{
    public class TransitionFunction
    {
        public Guid TransitionId { get; set; }
        public WorkFlowStepTransition Transition { get; set; } = null!;
        public Guid FunctionId { get; set; }
        public Function function { get; set; } = null!;
    }
}

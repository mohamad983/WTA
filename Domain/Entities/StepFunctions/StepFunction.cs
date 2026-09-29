using Domain.Entities.Functions;
using Domain.Entities.WorkFlowSteps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.StepFunctions
{
    public class StepFunction
    {
        public Guid StepId { get; set; }
        public WorkFlowStep Step { get; set; } = null!;
        public Guid FunctionId { get; set; }
        public Function function { get; set; } = null!;
    }
}

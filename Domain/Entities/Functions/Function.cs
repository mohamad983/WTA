using Domain.Common;
using Domain.Entities.Functions.Args;
using Domain.Entities.StepFunctions;
using Domain.Entities.TransitionFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Functions
{
    public class Function : BaseEntity
    {
        public Guid FunctionId { get; private set; } = new Guid();
        public string Title { get; set; } = string.Empty;
        public string Script { get; set; } = string.Empty;
        private readonly List<StepFunction> _steps = [];
        public IReadOnlyCollection<StepFunction> Steps => _steps.AsReadOnly();
        private readonly List<TransitionFunction> _transitions = [];
        public IReadOnlyCollection<TransitionFunction> Transitions => _transitions.AsReadOnly();
        private Function()
        {

        }
        public Function(FunctionArgs Args)
        {
            Title = Args.Title;
            Script = Args.Script;
        }
        public void Modify(FunctionArgs Args)
        {
            Title = Args.Title;
            Script = Args.Script;
        }
    }
}

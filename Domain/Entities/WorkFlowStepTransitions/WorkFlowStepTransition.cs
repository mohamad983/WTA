using Domain.Common;
using Domain.Entities.TransitionFunctions;
using Domain.Entities.WorkFlowSteps;
using Domain.Entities.WorkFlowStepTransitions.Args;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.WorkFlowStepTransitions
{
    public class WorkFlowStepTransition : BaseEntity
    {
        public Guid WorkFlowStepTransitionId { get; private set; } = new Guid();
        public Guid NextStepId {  get; private set; }
        public WorkFlowStep NextStep { get; private set; } = null!;
        public Guid CurrentStepId { get; private set; }
        public WorkFlowStep CurrentStep { get; private set; } = null!;
        private List<TransitionFunction> _functions = [];
        public IReadOnlyCollection<TransitionFunction> Functions => _functions.AsReadOnly();
        private WorkFlowStepTransition()
        {

        }
        public WorkFlowStepTransition(WorkFlowStepTransitionArgs Args)
        {
            NextStepId = Args.NextStep;
            CurrentStepId = Args.CurrentStep;
        }
        public void Modify(WorkFlowStepTransitionArgs Args)
        {
            NextStepId = Args.NextStep;
            CurrentStepId = Args.CurrentStep;
        }
    }
}

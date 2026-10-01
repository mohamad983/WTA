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
        //public Guid WorkFlowStepTransitionId { get; private set; } = new Guid();
        public Guid NextStepId {  get; private set; }
        public WorkFlowStep NextStep { get; private set; } = null!;
        public Guid CurrentStepId { get; private set; }
        public WorkFlowStep CurrentStep { get; private set; } = null!;
        public Guid ActionId { get; private set; }
        private List<TransitionFunction> _functions = [];
        public IReadOnlyCollection<TransitionFunction> Functions => _functions.AsReadOnly();
        private WorkFlowStepTransition()
        {

        }
        public WorkFlowStepTransition(WorkFlowStepTransitionArgs Args)
        {
            if (Args.NextStepId == Guid.Empty)
            {
                throw new ArgumentException("NextStepId is null!");
            }
            if (Args.CurrentStepId == Guid.Empty)
            {
                throw new ArgumentException("CurrentStepId is null");
            }
            if (Args.ActionId == Guid.Empty)
            {
                throw new ArgumentException("ActionId is null!");
            }
            NextStepId = Args.NextStepId;
            CurrentStepId = Args.CurrentStepId;
            ActionId = Args.ActionId;
        }
        public void Modify(WorkFlowStepTransitionArgs Args)
        {
            if (Args.NextStepId == Guid.Empty)
            {
                throw new ArgumentException("NextStepId is null!");
            }
            if (Args.CurrentStepId == Guid.Empty)
            {
                throw new ArgumentException("CurrentStepId is null");
            }
            NextStepId = Args.NextStepId;
            CurrentStepId = Args.CurrentStepId;
        }
    }
}

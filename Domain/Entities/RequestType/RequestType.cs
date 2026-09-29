using Domain.Common;
using Domain.Entities.WorkFlowStep;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.RequestType
{
    public class RequestType : BaseEntity
    {
        public string RequestTypeTitle { get; private set; } = string.Empty;
        public ICollection<WorkFlowStep.WorkFlowStep> Steps { get; private set; } = null!;
        public int Code { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public void SetRequestTypeTitle (string requestTypeTitle)
        {
            RequestTypeTitle = requestTypeTitle;
            SetModified();
        }
        public void AddStep(WorkFlowStep.WorkFlowStep step)
        {
            Steps.Add(step);
            SetModified();
        }
        public void SetDescription(string description)
        {
            Description = description;
            SetModified();
        }
    }
}

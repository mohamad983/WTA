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
        public List<WorkFlowStep.WorkFlowStep> Steps { get; private set; } = null!;
        

        public void SetRequestTypeTitle (string requestTypeTitle)
        {
            RequestTypeTitle = requestTypeTitle;
            SetModified();
        }
    }
}

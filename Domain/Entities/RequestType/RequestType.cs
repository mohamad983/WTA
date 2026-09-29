using Domain.Common;
using Domain.Entities.RequestType.Args;
using Domain.Entities.WorkFlowStep;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.RequestType
{
    public class RequestType : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public ICollection<WorkFlowStep.WorkFlowStep> Steps { get; private set; } = null!;
        public int Code { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public void ModifyRequestTypeTitle (string requestTypeTitle)
        {
            Title = requestTypeTitle;
            SetModified();
        }
        public void AddStep(WorkFlowStep.WorkFlowStep step)
        {
            Steps.Add(step);
            SetModified();
        }
        public void RemoveStep(WorkFlowStep.WorkFlowStep step)
        {
            Steps.Remove(step);
            SetModified();
        }
        public void ModifyDescription(string description)
        {
            Description = description;
            SetModified();
        }
        public void ModifyCode(int Code)
        {
            this.Code = Code;
        }
        private RequestType()
        {

        }
        public RequestType(RequestTypeArgs args)
        { 
            Title = args.Title;
            Description = args.Description;
            Code = args.Code;
        }
        public void Modify(RequestTypeArgs args)
        {
            Title = args.Title;
            Description = args.Description;
        }
    }
}

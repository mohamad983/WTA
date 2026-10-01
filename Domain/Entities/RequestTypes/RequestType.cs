using Domain.Common;
using Domain.Entities.Requests;
using Domain.Entities.RequestTypes.Args;
using Domain.Entities.WorkFlowSteps;

namespace Domain.Entities.RequestTypes
{
    public class RequestType : BaseEntity
    {
        public Guid RequestTypeGuid { get; private set; } = new Guid();
        public string Title { get; private set; } = string.Empty;
        private readonly List<WorkFlowStep> _steps = [];
        public IReadOnlyCollection<WorkFlowStep> Steps => _steps.AsReadOnly();
        private readonly List<Request> _request = [];
        public IReadOnlyCollection<Request> Requests => _request.AsReadOnly();
        public int Code { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public void ModifyRequestTypeTitle (string requestTypeTitle)
        {
            Title = requestTypeTitle;
            SetModified();
        }
        public void AddStep(WorkFlowStep step)
        {
            _steps.Add(step);
            SetModified();
        }
        public void AddRequest(Request request)
        {
            _request.Add(request);
            SetModified();
        }
        public void RemoveStep()
        {
            _request.Clear();
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

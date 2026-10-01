using Domain.Common;
using Domain.Entities.Requests;
using Domain.Entities.RequestTypes.Args;
using Domain.Entities.WorkFlowActions;
using Domain.Entities.WorkFlowSteps;

namespace Domain.Entities.RequestTypes
{
    public class RequestType : BaseEntity
    {
        //public Guid RequestTypeGuid { get; private set; } = new Guid();
        public string Title { get; private set; } = string.Empty;
        private readonly List<WorkFlowStep> _steps = [];
        public IReadOnlyCollection<WorkFlowStep> Steps => _steps.AsReadOnly();
        private readonly List<WorkFlowAction> _actions = [];
        public IReadOnlyCollection<WorkFlowAction> Actions => _actions.AsReadOnly();
        private readonly List<Request> _request = [];
        public IReadOnlyCollection<Request> Requests => _request.AsReadOnly();
        public int Code { get; private set; }
        public string Description { get; private set; } = string.Empty;
        private RequestType()
        {

        }
        public RequestType(RequestTypeArgs args)
        { 
            Title = args.Title;
            Description = args.Description;
            Code = args.Code;
        }
        public void AddRequest(Request request)
        {
            _request.Add(request);
            SetModified();
        }
        public void RemoveRequest()
        {
            _request.Clear();
        }
        public void Modify(RequestTypeArgs args)
        {
            
            Title = args.Title;
            Description = args.Description;
            SetModified();
        }
        public void AddStep(WorkFlowStep step)
        {
            _steps.Add(step);
        }
        public void RemoveStep()
        {
            _steps.Clear();
        }
        public void AddAction(WorkFlowAction action)
        {
            _actions.Add(action);
        }
        private WorkFlowAction CreateAction(string code,string title,bool isSystem)
        {
            if (_actions.Any(a => a.Code == code && !a.IsDeleted))
            {

            }
            var action = new WorkFlowAction(this.Id,code,title,isSystem);
            _actions.Add(action);
            return action;
        }
        private WorkFlowAction FindActiveAction(Guid actionId)
        {
            var result = _actions.FirstOrDefault(a => a.Id == actionId);
            return result;
        }
    }
}

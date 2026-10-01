using Domain.Common;
using Domain.Entities.Requests.Args;
using Domain.Entities.Requests.Enums;
using Domain.Entities.RequestTypes;
using Domain.Entities.Users;
using static Domain.Common.AppPermissions;

namespace Domain.Entities.Requests
{
    public class Request:BaseEntity
    {
        //public Guid RequestId { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public int Code { get; private set; }

        public string Description { get; private set; } = string.Empty;

        public int TrackingCode { get; private set; }

        public StatusEnum StatusEnum { get; private set; }

        public RequestType RequestType { get; private set; }

        public Guid RequestTypeId { get; private set; }

        private readonly List<RequestValue> _requestValue = [];
        //public byte[] RowVersion { get; private set; } = null!;

        public IReadOnlyCollection<RequestValue> RequestValues => _requestValue.AsReadOnly();

        private readonly List<RequestApproval> _requestApproval = [];
        //public byte[] RowVersion { get; private set; } = null!;

        public IReadOnlyCollection<RequestApproval> RequestApproval => _requestApproval.AsReadOnly();
        private Request() { }

        public Request(RequestArgs args)
        {
            Title = args.Title;
            Code = args.Code;
            Description = args.Description;
            StatusEnum=(StatusEnum)args.StatusEnum;
            RequestTypeId = args.RequestTypeId;
        }
        public static Request New(RequestArgs args)
        {
            return new Request(args);
        }
        public void AddRequestValue(RequestValue values)
        {
            _requestValue.Add(values);
        }
        public void ClearRequestValue()
        {
            _requestValue.Clear();
        }
        public void AddRequestApproval(RequestApproval approval)
        {
            _requestApproval.Add(approval);
        }
        public void ClearRequestApproval()
        {
            _requestApproval.Clear();
        }
        public void Modify(RequestArgs args)
        {
            Title = args.Title;
            Code = args.Code;
            Description = args.Description;
            StatusEnum = (StatusEnum)args.StatusEnum;
            RequestTypeId = args.RequestTypeId;
            SetModified();
        }

    }
}

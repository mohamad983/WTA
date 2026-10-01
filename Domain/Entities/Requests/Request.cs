using Domain.Common;
using Domain.Entities.Requests.Args;
using Domain.Entities.Requests.Enums;
using Domain.Entities.RequestTypes;

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

        public int RequestTypeId { get; private set; }
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
        public void Modify(RequestArgs args)
        {
            Title = args.Title;
            Code = args.Code;
            Description = args.Description;
            StatusEnum = (StatusEnum)args.StatusEnum;
            RequestTypeId = args.RequestTypeId;
        }

    }
}

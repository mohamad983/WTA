using Domain.Common;
using Domain.Entities.Requests.Args;

namespace Domain.Entities.Requests
{
    public class RequestApproval:BaseEntity
    {
            public Guid TargetStepId { get; private set; }
            public Guid ApproverUserId { get; private set; }

            public Guid ActionId { get; private set; }

        public Request Request { get; private set; }
        public Guid RequestId { get; private set; }

        public string Comment { get; private set; }

        private RequestApproval()
        {

        }

        public RequestApproval(RequestApprovalArgs args)
        {
            TargetStepId=args.TargetStepId;
            ApproverUserId=args.ApproverUserId;
            ActionId=args.ActionId;
            Comment = args.Comment;
            RequestId = args.RequestId;
        }
        public static RequestApproval New(RequestApprovalArgs args)
        {
            return new RequestApproval(args);
        }
        public void Modify(RequestApprovalArgs args)
        {
            TargetStepId = args.TargetStepId;
            ApproverUserId = args.ApproverUserId;
            ActionId = args.ActionId;
            Comment = args.Comment;
            RequestId = args.RequestId;
            SetModified();
        }

        
    }
}

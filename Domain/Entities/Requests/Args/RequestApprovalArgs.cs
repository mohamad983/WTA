namespace Domain.Entities.Requests.Args
{
    public class RequestApprovalArgs
    {
        public Guid TargetStepId { get;  set; }
        public Guid ApproverUserId { get;  set; }

        public Guid RequestId { get;  set; }

        public Guid ActionId { get;  set; }

        public string Comment { get;  set; }
    }
}

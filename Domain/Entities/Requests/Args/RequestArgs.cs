namespace Domain.Entities.Requests.Args
{
    public class RequestArgs
    {
        public string Title { get;  set; }

        public int Code { get;  set; }

        public string Description { get;  set; }

        public short StatusEnum { get; set; }

        public int RequestTypeId { get;  set; }
    }
}

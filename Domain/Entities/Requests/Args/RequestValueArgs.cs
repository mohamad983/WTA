namespace Domain.Entities.Requests.Args
{
    public class RequestValueArgs
    {
        public Guid RequestId { get;  set; } 
        public string Value { get;  set; } = null!;
    }
}

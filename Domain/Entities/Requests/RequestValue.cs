using Domain.Common;

namespace Domain.Entities.Requests
{
    public class RequestValue:BaseEntity
    {
        public Guid RequestValueId { get; private set; }

        public string Value { get; private set; }

    }
}

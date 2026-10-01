using Domain.Common;
using Domain.Entities.Requests.Args;

namespace Domain.Entities.Requests
{
    public class RequestValue:BaseEntity
    {
        //public Guid RequestValueId { get; private set; }

        public string Value { get; private set; } = null!;

        public Request Request { get; private set; }
        public Guid RequestId { get; private set; }

        
      

       private RequestValue()
       {

       }

        public RequestValue(RequestValueArgs args)
        {
            Value = args.Value;
            RequestId = args.RequestId;
        }

        public static RequestValue New(RequestValueArgs args)
        {
            return new RequestValue(args);
        }
        public void Modify(RequestValueArgs args)
        {
            Value = args.Value;
            RequestId = args.RequestId;
            SetModified();
        }

        

    }
}

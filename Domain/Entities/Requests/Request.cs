using Domain.Common;
using Domain.Entities.Requests.Args;

namespace Domain.Entities.Requests
{
    public class Request:BaseEntity
    {
        public Guid RequestId { get; private set; }

        public string Title { get; private set; }

        public int Code { get; private set; }

        public string Description { get; private set; }

        public int TrackingCode { get; private set; }

       



        private Request() { }

        public Request(RequestArgs args)
        {
            Title = args.Title;
            Code = args.Code;
            Description = args.Description;
       
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
           
        }

    }
}

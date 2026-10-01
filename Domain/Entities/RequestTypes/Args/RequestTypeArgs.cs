using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Domain.Entities.RequestTypes.Args
{
    public class RequestTypeArgs
    {
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Code { get; set; }
       // public Guid ActionId { get; set; }
    }
}

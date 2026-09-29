using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.RequestLogs.Args
{
    public class RequestLogArgs
    {
        public string IpAddress { get; set; } = string.Empty;
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }

    }
}

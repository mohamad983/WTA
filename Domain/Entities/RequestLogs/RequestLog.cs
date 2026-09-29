using Domain.Common;
using Domain.Entities.RequestLogs.Args;
using Domain.Entities.Requests;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.RequestLogs
{
    public class RequestLog : BaseEntity
    {
        public Guid LogId { get; private set; } = new Guid();
        public string LogMessage { get; private set; } = string.Empty;
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;
        public Guid RequestId { get; private set; }
        public Request Request { get; private set; } = null!;
        public string IpAdress { get; private set; } = string.Empty;

        private RequestLog()
        {

        }
        public RequestLog(RequestLogArgs Args)
        {
            UserId = Args.UserId;
            RequestId = Args.RequestId;
            IpAdress = Args.IpAddress;
        }
        /*public void Modify(RequestLogArgs Args)
        {
            UserId = Args.UserId;
            RequestId = Args.RequestId;
            IpAdress = Args.IpAddress;
        }*/
        public void MakeMessage()
        {

        }
    }
}

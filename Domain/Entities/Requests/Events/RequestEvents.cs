using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Requests.Events
{
    public sealed record RequestCreated(Guid RequestId,string title,int code);
    public sealed record RequestDeleted(Guid RequestId);
    
}

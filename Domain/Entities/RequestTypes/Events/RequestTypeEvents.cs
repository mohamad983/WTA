using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.RequestTypes.Events
{
    public sealed record RequestTypeCreated(Guid requestTypeId, string title) : IDomainEvent;
    public sealed record RequestTypeDeleted(Guid requestTypeId) : IDomainEvent;
    public sealed record RequestTypeRenamed(Guid requestTypeId, string NewTitle) : IDomainEvent;
}

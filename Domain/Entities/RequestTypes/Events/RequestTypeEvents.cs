using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.RequestTypes.Events
{
    public sealed record RequestTypeCreated(Guid requestTypeId, string title) : IDomainEvent;
    public sealed record RequestTypeDeleted(Guid requestTypeId) : IDomainEvent;
    public sealed record RequestTypeRenamed(Guid requestTypeId, string NewTitle) : IDomainEvent;
    public sealed record RequestTypeActivated (Guid requestTypeId) : IDomainEvent;
    public sealed record RequestTypeDeactivated(Guid requestTypeId) : IDomainEvent;

    public sealed record WorkFlowActionAdded(Guid RequestTypeId, string RequestTypeTitle,Guid ActionId ,
        string ActionCode, string ActionTitle) : IDomainEvent;
    public sealed record WorkFlowActionRemoved(Guid RequestTypeId, Guid ActionId) : IDomainEvent;
    public sealed record WorkFlowActionRenamed(Guid RequestTypeId, Guid ActionId, string NewTitle) : IDomainEvent;
}

using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Users.Events
{
    public sealed record RolePermissionsChanged(Guid RoleId) : IDomainEvent;
    public sealed record RoleDeleted(Guid RoleId) : IDomainEvent;
    public sealed record UserRolesChanged(Guid UserId) : IDomainEvent;
}

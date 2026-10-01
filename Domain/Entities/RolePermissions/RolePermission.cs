using Domain.Entities.Permissions;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.RolePermissions
{
    public class RolePermission
    {
        public Guid RoleId { get; private set; }
        public Role Role { get; private set; } = null!;
        public Guid PermissionId { get; private set; }
        public Permission Permission { get; private set; } = null!;
        public DateTime GrantedAt { get; private set; } = DateTime.UtcNow;
        private RolePermission()
        {

        }
        public RolePermission(Guid RoleId,Guid PermissionId)
        {
            if (RoleId == Guid.Empty)
            {
                throw new ArgumentException("Role Id is empty!");
            }
            if (PermissionId == Guid.Empty)
            {
                throw new ArgumentException("Permission Id is empty!");
            }
            this.RoleId = RoleId;
            this.PermissionId = PermissionId;
        }
    }
}

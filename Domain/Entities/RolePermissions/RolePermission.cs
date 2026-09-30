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
        private RolePermission()
        {

        }
        public RolePermission(Guid RoleId,Guid PermissionId)
        {
            this.RoleId = RoleId;
            this.PermissionId = PermissionId;
        }
    }
}

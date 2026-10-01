using Domain.Common;
using Domain.Entities.Permissions;
using Domain.Entities.RolePermissions;
using Domain.Entities.Users.Args;

namespace Domain.Entities.Users
{
    public class Role : BaseEntity
    {
        //public Guid RoleId { get; private set; }
        public bool IsSystem { get; private set; }
        public string RoleName { get; private set; } = string.Empty;

        public int RoleImportance { get; private set; }

        private readonly List<User> _user = [];

        public IReadOnlyCollection<User> Users => _user.AsReadOnly();
        private readonly List<RolePermission> _permissions = [];
        public IReadOnlyCollection<RolePermission> Permissions => _permissions.AsReadOnly();

        private Role() { }

        public Role(RoleArgs args)
        {
            //RoleId = Guid.NewGuid();
            IsSystem = args.isSystem;
            RoleName=args.RoleName;
            RoleImportance=args.RoleImportance;
            //_user.Add(args.User);
        }

        public void Modify(RoleArgs args)
        {
            RoleName = args.RoleName;
            RoleImportance = args.RoleImportance;
        }
        public void AddPermission(Guid permissionId)
        {
            if (_permissions.Any(p => p.PermissionId == permissionId))
            {
                return;
            }
            _permissions.Add(new RolePermission(this.Id,permissionId));
        }
        public void RemovePermission(Guid permissionId)
        {
            if (IsSystem == true)
            {
                throw new InvalidOperationException("You cannot take away the permissions of a system admin!");
            }
            var permission = _permissions.FirstOrDefault(rp => rp.PermissionId == permissionId);
            if (permission == null)
            {
                throw new NotImplementedException();
            }
            _permissions.Remove(permission);
        }
        public static Role New(RoleArgs args)
        {
            return new Role(args);
        }
    }
}

using Domain.Common;
using Domain.Entities.Permissions;
using Domain.Entities.RolePermissions;
using Domain.Entities.Users.Args;
using Domain.Entities.Users.Events;

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
            if (string.IsNullOrWhiteSpace(args.RoleName))
            {
                throw new ArgumentException("Role name cannot be empty!");
            }
            if (args.RoleImportance < 0)
            {
                throw new DomainException("Role importance cant be negative");
            }
            //RoleId = Guid.NewGuid();
            IsSystem = args.isSystem;
            RoleName=args.RoleName.Trim();
            RoleImportance=args.RoleImportance;
            //_user.Add(args.User);
        }

        public void Modify(RoleArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.RoleName))
            {
                throw new ArgumentException("Role name cannot be empty!");
            }
            if (args.RoleImportance < 0)
            {
                throw new DomainException("Role importance cant be negative");
            }
            RoleName = args.RoleName.Trim();
            RoleImportance = args.RoleImportance;
        }
        public void AddRolePermission(RolePermission rolePermission)
        {
            if (rolePermission.RoleId != Id)
            {
                throw new DomainException("This permission isnt meant for this role!");
            }
            if (_permissions.Any(p => p.PermissionId == rolePermission.PermissionId))
            {
                return;
            }
            _permissions.Add(rolePermission);
            AddDomainEvent(new RolePermissionsChanged(Id));
        }
        public void RemovePermission(Guid permissionId)
        {
            if (IsSystem == true)
            {
                throw new InvalidOperationException("You cannot take away the permissions of a system admin!");
            }
            var permission = _permissions.SingleOrDefault(rp => rp.PermissionId == permissionId);
            if (permission == null)
            {
                return;
            }
            _permissions.Remove(permission);
            AddDomainEvent(new RolePermissionsChanged(Id));
        }
        public static Role New(RoleArgs args)
        {
            return new Role(args);
        }
    }
}

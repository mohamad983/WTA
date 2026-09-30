using Domain.Common;
using Domain.Entities.Permissions;
using Domain.Entities.RolePermissions;
using Domain.Entities.Users.Args;

namespace Domain.Entities.Users
{
    public class Role : BaseEntity
    {
        //public Guid RoleId { get; private set; }
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

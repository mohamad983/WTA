using Domain.Entities.Users.Args;

namespace Domain.Entities.Users
{
    public class Role
    {
        public Guid RoleId { get; private set; }
        public string RoleName { get; private set; }

        public int RoleImportance { get; private set; }

        private readonly List<User> _user = [];

        public IReadOnlyCollection<User> Users => _user.AsReadOnly();

        private Role() { }

        public Role(RoleArgs args)
        {
            RoleId = Guid.NewGuid();
            RoleName=args.RoleName;
            RoleImportance=args.RoleImportance;
            _user.Add(args.User);
        }

        public static Role New(RoleArgs args)
        {
            return new Role(args);
        }
    }
}

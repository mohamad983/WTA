using Domain.Common;
using Domain.Entities.Users.Args;

namespace Domain.Entities.Users
{
    public class User: BaseEntity
    {
        //public Guid UserId { get; private set; } //primary key
        public string UserName { get; private set; } = string.Empty;
        public string FullName { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        private readonly List<Role> _roles = [];
        public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
        private User() { }

        public User(UserArgs args)
        {
            if(string.IsNullOrWhiteSpace(args.UserName))
            {
                throw new DomainException("User Name is invalid!");
            }
            if (string.IsNullOrWhiteSpace(args.Email))
            {
                throw new DomainException("Email is invalid!");
            }
            if (string.IsNullOrWhiteSpace(args.FirstName))
            {
                throw new DomainException("First Name is invalid!");
            }
            if (string.IsNullOrWhiteSpace(args.LastName))
            {
                throw new DomainException("Last Name is invalid!");
            }
            if (string.IsNullOrWhiteSpace(args.PasswordHash))
            {
                throw new DomainException("Password Hash is invalid!");
            }
            UserName =args.UserName;
            Email=args.Email;
            FirstName = args.FirstName.Trim();
            LastName = args.LastName.Trim();
            PasswordHash = args.PasswordHash;
            FullName = $"{FirstName} {LastName}";
        }
        public static User New(UserArgs args)
        {
            return new User(args);
        }
        public void AddRole(Role role)
        {
            _roles.Add(role);
        }
        public void ClearRole(Role role)
        {
            _roles.Clear();
        }
        public void Modify(UserArgs args)
        {
            if(string.IsNullOrWhiteSpace(args.UserName))
            {
                throw new DomainException("User Name is invalid!");
            }
            if (string.IsNullOrWhiteSpace(args.Email))
            {
                throw new DomainException("Email is invalid!");
            }
            if (string.IsNullOrWhiteSpace(args.FirstName))
            {
                throw new DomainException("First Name is invalid!");
            }
            if (string.IsNullOrWhiteSpace(args.LastName))
            {
                throw new DomainException("Last Name is invalid!");
            }
            if (string.IsNullOrWhiteSpace(args.PasswordHash))
            {
                throw new DomainException("Password Hash is invalid!");
            }
            UserName = args.UserName;
            Email= args.Email;
            FirstName = args.FirstName.Trim();
            LastName = args.LastName.Trim();
            PasswordHash = args.PasswordHash;
            FullName = $"{FirstName} {LastName}";
        }
        public void AssignRole(Role role)
        {
            ArgumentNullException.ThrowIfNull(role);

            if (role.IsDeleted)
                throw new DomainException("A deleted role can't be assigned.");
            if (_roles.Any(r => r.Id == role.Id))
                return;                               // already assigned

            _roles.Add(role);
        }
        public void RemoveRole(Guid roleId)
        {
            var role = _roles.SingleOrDefault(r => r.Id == roleId);
            if (role is null) return;

            _roles.Remove(role);
        }
    }
}

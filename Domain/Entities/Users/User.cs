using Domain.Common;
using Domain.Entities.Users.Args;

namespace Domain.Entities.Users
{
    public class User: BaseEntity
    {
        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public string FullName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        private readonly List<Role> _roles = [];
        public byte[] RowVersion { get; private set; } = null!;

        public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }
        private User() { }

        public User(UserArgs args)
        {
            UserId=Guid.NewGuid();
            UserName=args.UserName;
            _roles.Add(args.Role);
            FullName = args.FullName;
            Email=args.Email;
            FirstName = args.FirstName;
            LastName = args.LastName;
            PasswordHash = args.PasswordHash;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public static User New(UserArgs args)
        {
            return new User(args);
        }

        public void Modify(UserArgs args)
        {
            UserName = args.UserName;
            FullName = args.FullName;
            FirstName = args.FirstName;
            Email = args.Email;
            _roles.Add(args.Role);
            LastName = args.LastName;
            UpdatedAt = DateTime.UtcNow;
        }
        public void ModifyPassword(string password)
        {
            PasswordHash = password;
            UpdatedAt = DateTime.UtcNow;
        }
        public void ModifyEmail(string email)
        {
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }
        //val
    }
}

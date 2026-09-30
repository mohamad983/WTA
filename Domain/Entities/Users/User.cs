using Domain.Common;
using Domain.Entities.Users.Args;

namespace Domain.Entities.Users
{
    public class User: BaseEntity
    {
        //public Guid UserId { get; private set; } //primary key
        public string UserName { get; private set; }
        public string FullName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        private readonly List<Role> _roles = [];
        //public byte[] RowVersion { get; private set; } = null!;

        public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

        //public DateTime CreatedAt { get; private set; }

        //public DateTime UpdatedAt { get; private set; }
        private User() { }

        public User(UserArgs args)
        {
            //UserId=Guid.NewGuid();
            UserName=args.UserName;
            _roles.Add(args.Role);
            FullName = args.FullName;
            Email=args.Email;
            FirstName = args.FirstName;
            LastName = args.LastName;
            PasswordHash = args.PasswordHash;
            //UpdatedAt = DateTime.UtcNow;
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
            SetModified();
        }
        public void ModifyPassword(string password)
        {
            PasswordHash = password;
            SetModified();
        }
        public void ModifyEmail(string email)
        {
            Email = email;
            SetModified();
        }
        public void ModifyUserName(string userName)
        {
            UserName = userName;
            SetModified();
        }
        public void ModifyFirstName(string firstName)
        {
            FirstName = firstName;
            SetModified();
        }
        public void ModiftLastName (string lastName)
        {
            LastName = lastName;
            SetModified();
        }
        public void AddtoRole(Role role)
        {
            _roles.Add(role);
        }
        public void RemoveFromRole(Role role)
        {
            _roles.Remove(role);
        }
        //val
    }
}

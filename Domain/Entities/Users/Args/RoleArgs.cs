    namespace Domain.Entities.Users.Args
    {
        public class RoleArgs
        {
            public string RoleName { get;  set; }

            public int RoleImportance { get;  set; }

            public User User { get; set; }
            public bool isSystem {  get; set; }

        }
    }

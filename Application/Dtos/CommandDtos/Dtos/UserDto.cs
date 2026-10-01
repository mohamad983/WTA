using Domain.Entities.Users;

namespace Application.Dtos.CommandDtos.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}

using Application.Dtos.CommandDtos;
using Domain.Entities.Users.Args;

namespace Application.CommandHandlers.UserCommandHandlers.mapper
{
    public static class UserMap
    {
        public static UserArgs Map(UserCommandDto dto,string HashedPassword)
        {
            return new UserArgs
            {
                UserName = dto.UserName,
                PasswordHash = HashedPassword,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

        }
       
    }
    
}

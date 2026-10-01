using Application.Common.CqrsPattern.CommandAndQuery;
using Application.Dtos.CommandDtos.Dtos;
using MediatR;

namespace Application.Dtos.CommandDtos
{
    public class UserCommandDto:ICommand<Unit>
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Guid> RoleIds { get; set; }
    }
}

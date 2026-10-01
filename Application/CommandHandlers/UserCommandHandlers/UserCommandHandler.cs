using Application.Common.CqrsPattern.CommandAndQueryHandler;
using Application.Dtos.CommandDtos;
using Domain.Entities.Users;
using Domain.RepoContracts;
using MediatR;

namespace Application.CommandHandlers.UserCommandHandlers
{
    public class UserCommandHandler : ICommandHandler<UserCommandDto, Unit>
    {
        private readonly IUserRepo userRepo;

        public UserCommandHandler(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public Task<Unit> Handle(UserCommandDto request, CancellationToken cancellationToken)
        {
           throw new NotImplementedException(); 
        }
    }
}

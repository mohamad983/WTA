using Application.CommandHandlers.UserCommandHandlers.mapper;
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

        public async Task<Unit> Handle(UserCommandDto request, CancellationToken cancellationToken)
        {
            //not implemented yet

            var dataUser = UserMap.Map(request,"123");
            return Unit.Value;
        }
    }
}

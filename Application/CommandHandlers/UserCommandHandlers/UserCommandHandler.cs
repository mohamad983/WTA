using Application.Common.CqrsPattern.CommandAndQueryHandler;
using Application.Dtos.CommandDtos;
using MediatR;

namespace Application.CommandHandlers.UserCommandHandlers
{
    public class UserCommandHandler : ICommandHandler<UserCommandDto, Unit>
    {
        public Task<Unit> Handle(UserCommandDto request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

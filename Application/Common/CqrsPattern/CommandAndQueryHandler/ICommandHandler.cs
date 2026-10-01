using Application.Common.CqrsPattern.CommandAndQuery;
using MediatR;

namespace Application.Common.CqrsPattern.CommandAndQueryHandler
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
         where TCommand : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }

}

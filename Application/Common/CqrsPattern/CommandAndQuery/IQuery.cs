using MediatR;

namespace Application.Common.CqrsPattern.CommandAndQuery
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}


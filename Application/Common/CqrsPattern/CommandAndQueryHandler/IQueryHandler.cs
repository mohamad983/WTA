using Application.Common.CqrsPattern.CommandAndQuery;
using MediatR;

namespace Application.Common.CqrsPattern.CommandAndQueryHandler
{
    public interface IQueryHandler
    {
        public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        {
        }
    }
}

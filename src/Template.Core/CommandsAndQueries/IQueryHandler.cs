using MediatR;

namespace Template.Shared.Core.CommandsAndQueries
{
    public interface IQueryHandler<in TQuery, TResponse> :
        IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}

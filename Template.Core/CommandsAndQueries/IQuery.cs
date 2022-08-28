using MediatR;

namespace Template.Shared.Core.CommandsAndQueries
{
    public interface IQuery<out TResult> : IRequest<TResult> { }
}

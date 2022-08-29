using MediatR;

namespace Template.Shared.Core.CommandsAndQueries
{
    public interface ICommand<out TResult> : IRequest<TResult> { }
}

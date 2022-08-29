using MediatR;

namespace Template.Shared.Core.CommandsAndQueries
{
    public interface ICommandHandler<in TCommand, TResponse> :
        IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
    }
}

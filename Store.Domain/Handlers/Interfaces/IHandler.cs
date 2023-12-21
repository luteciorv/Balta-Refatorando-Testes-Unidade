using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Handlers.Interfaces;

public interface IHandler<TCommand> where TCommand : ICommand
{
    ICommandResult Handle(TCommand command);
}

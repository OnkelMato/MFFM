using System.Windows.Input;

namespace Mffm.Contracts;

public interface ICommandResolver
{
    ICommand ResolveCommand<TCommand>()
        where TCommand : ICommand;
}
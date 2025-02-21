using System.Windows.Input;

namespace Mffm.Contracts
{
    /// <summary>
    /// Provides a method to resolve commands of a specified type.
    /// </summary>
    public interface ICommandResolver
    {
        /// <summary>
        /// Resolves a command of the specified type.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command to resolve.</typeparam>
        /// <returns>An instance of the resolved command.</returns>
        ICommand ResolveCommand<TCommand>()
            where TCommand : ICommand;
    }
}
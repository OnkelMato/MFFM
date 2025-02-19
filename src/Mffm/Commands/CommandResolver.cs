using System.Windows.Input;
using Mffm.Contracts;

namespace Mffm.Commands
{
    internal class CommandResolver(IServiceProvider serviceProvider) : ICommandResolver
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        public ICommand ResolveCommand<TCommand>()
            where TCommand : ICommand
        {

            var command = (ICommand)_serviceProvider.GetService(typeof(TCommand))!;
            if (command == null)
            {
                throw new InvalidOperationException($"Command '{typeof(TCommand).Name}' not found.");
            }

            return command;
        }
    }
}

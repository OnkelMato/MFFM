using System.Windows.Input;

namespace Mffm.Contracts;

/// <summary>
/// Adds UI information to a command.
/// </summary>
public interface IExtendedCommand : ICommand
{
    /// <summary>
    /// Gets or sets the icon for the command.
    /// </summary>
    Image? Icon { get; }

    /// <summary>
    /// Gets the shortcut keys for the command.
    /// </summary>
    Keys? ShortcutKeys { get; }
}
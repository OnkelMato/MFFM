using System.Windows.Input;
using Mffm.Contracts;
using Mffm.Core;
using YxtEditor.Essential.Messages;
using YxtEditor.Essential.Models;

namespace YxtEditor.Essential.Commands;

internal class OpenDocumentCommand(IEventAggregator eventAggregator, IEnumerable<IFileTypeSupport> fileTypes) : ICommand
{
    private readonly IEventAggregator _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
    private readonly IEnumerable<IFileTypeSupport> _fileTypes = fileTypes ?? throw new ArgumentNullException(nameof(fileTypes));

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        var dlg = new OpenFileDialog();

        dlg.Filter = _fileTypes
            .Aggregate(
                string.Empty,
                (current, fileType) => $@"{current}|{fileType.Name} (*{fileType.Extension})|*{fileType.Extension}")
            .TrimStart('|');

        var answer = dlg.ShowDialog();

        if (answer == DialogResult.OK)
        {
            var fileTypeSupport = _fileTypes.FirstOrDefault(x => dlg.FileName.EndsWith(x.Extension));

            var document = fileTypeSupport.LoadFromFile(dlg.FileName);

            _eventAggregator.Publish(new DocumentLoaded(document));
            _eventAggregator.Publish(new LogMessage("Document opened"));
        }
    }

    public event EventHandler? CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
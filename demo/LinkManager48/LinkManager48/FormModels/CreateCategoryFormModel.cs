using System;
using System.Windows.Input;
using LinkManager48.Messages;
using Mffm.Commands;
using Mffm.Contracts;
using Mffm.Core;

namespace LinkManager48.FormModels
{
    internal class CreateCategoryFormModel : Mffm.Contracts.IFormModel
    {
        private readonly IEventAggregator _eventAggregator;

        public CreateCategoryFormModel(
            ICommandResolver commandResolver,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            Ok = new CompositeCommand(
                new FunctionToCommandAdapter(ctx => PublishCategory()),
                commandResolver.ResolveCommand<CloseFormCommand>());
            Cancel = commandResolver.ResolveCommand<CloseFormCommand>();
        }

        private void PublishCategory()
        {
            _eventAggregator.Publish(new CategoryAddedMessage(CategoryName));
        }

        public string CategoryName { get; set; }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mffm.Contracts;

namespace LinkManager48.MffmExtensions
{
    internal class FormAdapter : IWindowManagerAdapter
    {
        private readonly IWindowManager _windowManager;

        public FormAdapter(IWindowManager windowManager)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
        }

        public void Initialize(IFormModel formModel)
        {
        }
    }

    internal interface IWindowManagerAdapter
    {
        void Initialize(IFormModel formModel);
    }
}

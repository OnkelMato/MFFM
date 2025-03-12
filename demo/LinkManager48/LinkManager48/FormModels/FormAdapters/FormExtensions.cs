using System.Windows.Forms;

namespace LinkManager48.FormModels.FormAdapters
{
    public static class FormExtensions
    {
        public static TControl Get<TControl>(this Form form)
            where TControl : Control
        {
            var controlType = typeof(TControl).FullName;
            foreach (var ctrl in form.Controls)
                if (ctrl.GetType().FullName == controlType)
                    return (TControl)ctrl;

            return null;
        }
    }
}
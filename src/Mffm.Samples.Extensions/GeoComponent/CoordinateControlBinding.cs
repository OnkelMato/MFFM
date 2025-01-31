using Mffm.Contracts;

namespace Mffm.Samples.Extensions.GeoComponent;

public class CoordinateControlBinding : IControlBinding
{
    public bool Bind(Control control, IFormModel formModel)
    {
        if (control is not GeolocationControl geoControl) return false;

        control.DataBindings.Add(new Binding(nameof(geoControl.Coordinate), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged));
        return true;
    }
}
using System.ComponentModel;
using System.Globalization;

namespace Mffm.Samples.Extensions.GeoComponent;

public partial class GeolocationControl : UserControl
{
    private Coordinate _coordinate = new Coordinate();

    [DefaultValue(typeof(Coordinate), null)]
    public Coordinate Coordinate
    {
        get => _coordinate;
        set
        {
            if (Equals(_coordinate, value)) return;

            _coordinate = value;

            latitudeTextbox.Text = _coordinate.Latitude.ToString(CultureInfo.CurrentCulture);
            longitudeTextbox.Text = _coordinate.Longitude.ToString(CultureInfo.CurrentCulture);
        }
    }

    public GeolocationControl()
    {
        InitializeComponent();
    }

    private void latitudeTextbox_TextChanged(object sender, EventArgs e)
    {
        Coordinate.Latitude = double.Parse(latitudeTextbox.Text, CultureInfo.CurrentCulture);
    }

    private void longitudeTextbox_TextChanged(object sender, EventArgs e)
    {
        Coordinate.Longitude = double.Parse(longitudeTextbox.Text, CultureInfo.CurrentCulture);
    }
}
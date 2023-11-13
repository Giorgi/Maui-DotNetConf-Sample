using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace SpatialData.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        private void NeighborhoodSelectedIndexChanged(object sender, EventArgs e)
        {
            var neighborhood = ((MainPageViewModel)BindingContext).GetSelectedNeighborhood();

            if (neighborhood == null)
            {
                return;
            }

            Map.MapElements.Clear();

            foreach (var geometry in neighborhood.Geometry)
            {
                var polygon = new Polygon
                {
                    StrokeWidth = 8,
                    StrokeColor = Color.FromArgb("#1BA1E2"),
                    FillColor = Color.FromArgb("#881BA1E2"),
                };

                foreach (var coordinate in geometry.Boundary.Coordinates)
                {
                    polygon.Geopath.Add(coordinate.ToLocation());
                }

                Map.MapElements.Add(polygon);
            }

            Map.MoveToRegion(new MapSpan(neighborhood.Geometry.Centroid.Coordinate.ToLocation(), 0.1, 0.1));
        }
    }
}
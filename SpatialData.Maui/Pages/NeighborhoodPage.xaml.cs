using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using SpatialData.Maui.ViewModels;
using System.ComponentModel;

namespace SpatialData.Maui.Pages
{
    public partial class NeighborhoodPage : ContentPage
    {
        public NeighborhoodPage(NeighborhoodPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        private NeighborhoodPageViewModel ViewModel => (NeighborhoodPageViewModel)BindingContext;

        protected override void OnAppearing()
        {
            ViewModel.PropertyChanged += ViewModelPropertyChanged;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            ViewModel.PropertyChanged -= ViewModelPropertyChanged;
            base.OnDisappearing();
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(ViewModel.SelectedNeighborhood) or nameof(ViewModel.SelectedBorough))
            {
                DrawNeighborhood();
            }
        }

        private void DrawNeighborhood()
        {
            Map.MapElements.Clear();
         
            var neighborhood = ViewModel.GetSelectedNeighborhood();

            if (neighborhood == null)
            {
                return;
            }

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

            Map.MoveToRegion(new MapSpan(neighborhood.Geometry.Centroid.Coordinate.ToLocation(), 0.06, 0.06));
        }
    }
}
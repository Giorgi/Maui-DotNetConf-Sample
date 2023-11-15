using System.ComponentModel;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using SpatialData.Maui.ViewModels;

namespace SpatialData.Maui.Pages;

public partial class NearestSubwaysPage : ContentPage
{
    public NearestSubwaysPage(NearestSubwaysPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

        Map.MoveToRegion(new MapSpan(new Location(40.758896, -73.985130), 0.1, 0.1));
    }

    private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.Radius) || e.PropertyName == nameof(ViewModel.Location))
        {
            DrawCircle();
        }
    }

    private NearestSubwaysPageViewModel ViewModel => (NearestSubwaysPageViewModel)BindingContext;

    private void DrawCircle()
    {
        Map.MapElements.Clear();

        var circle = new Circle
        {
            Center = ViewModel.Location,
            Radius = new Distance(ViewModel.Radius),
            StrokeColor = Color.FromArgb("#88FF0000"),
            StrokeWidth = 8,
            FillColor = Color.FromArgb("#88FFC0CB")
        };

        Map.MapElements.Add(circle);

        Map.MoveToRegion(new MapSpan(ViewModel.Location, 0.03, 0.03));
    }

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
}
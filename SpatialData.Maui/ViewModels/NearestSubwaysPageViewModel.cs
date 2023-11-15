using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;
using SpatialData.Maui.ApiClient;

namespace SpatialData.Maui.ViewModels;

public partial class NearestSubwaysPageViewModel : ObservableObject
{
    private readonly INewYorkServiceClient client;

    public NearestSubwaysPageViewModel(INewYorkServiceClient client)
    {
        this.client = client;

        Radius = 500;
        Location = new Location(40.758896, -73.985130);
        SwitchViewText = "Show as list";
    }

    [ObservableProperty]
    private ObservableCollection<Pin> nearestSubways = new ObservableCollection<Pin>();

    [ObservableProperty]
    private Location location;

    [ObservableProperty]
    private double radius;

    [ObservableProperty]
    private string switchViewText;

    [ObservableProperty]
    private bool isMapView = true;

    [ObservableProperty]
    private List<SubwayStation> stations;

    [RelayCommand]
    private async Task MapClicked(MapClickedEventArgs args)
    {
        Location = args.Location;

        await UpdateNearestSubways();
    }

    [RelayCommand]
    private async Task DragCompleted()
    {
        await UpdateNearestSubways();
    }

    private async Task UpdateNearestSubways()
    {
        NearestSubways.Clear();

        Stations = await client.GetNearestSubways(Location.Longitude, Location.Latitude, Radius);

        NearestSubways = new ObservableCollection<Pin>(Stations.Select(s => new Pin
        {
            Location = s.Location.Coordinate.ToLocation(),
            Label = $"{s.Name} - Routes: {s.Routes}",
            Address = s.LongName,
        }));
    }

    [RelayCommand]
    private void SwitchView()
    {
        IsMapView = !IsMapView;
        SwitchViewText = IsMapView ? "Show as list" : "Show on map";
    }
}
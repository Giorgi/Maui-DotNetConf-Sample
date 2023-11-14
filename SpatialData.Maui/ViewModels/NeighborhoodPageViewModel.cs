using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using NetTopologySuite.Geometries;
using Location = Microsoft.Maui.Devices.Sensors.Location;
using Point = NetTopologySuite.Geometries.Point;

namespace SpatialData.Maui.ViewModels;

public partial class NeighborhoodPageViewModel : ObservableObject
{
    private readonly INewYorkServiceClient client;

    public NeighborhoodPageViewModel(INewYorkServiceClient client)
    {
        this.client = client;
    }

    public List<string> Boroughs { get; private set; } = new List<string>
    {
        "Brooklyn",
        "Manhattan",
        "Queens",
        "Staten Island",
        "The Bronx"
    };

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ShowSubwaysCommand))]
    private string selectedBorough;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ShowSubwaysCommand))]
    private string selectedNeighborhood;

    [ObservableProperty]
    private List<string> neighborhoodNames;

    private List<Neighborhood> neighborhoods;

    [ObservableProperty]
    private ObservableCollection<Pin> subways = new ObservableCollection<Pin>();

    async partial void OnSelectedBoroughChanged(string value)
    {
        neighborhoods = await client.GetNeighborhoods(value);
        NeighborhoodNames = neighborhoods.Select(n => n.Name).ToList();
    }

    public Neighborhood GetSelectedNeighborhood()
    {
        if (SelectedNeighborhood == null)
        {
            return null;
        }

        var neighborhood = neighborhoods.First(n => n.Name == SelectedNeighborhood);
        return neighborhood;
    }

    [RelayCommand(CanExecute = nameof(CanShowSubways))]
    private async Task ShowSubways()
    {
        Subways.Clear();
        var stations = await client.GetSubwayStations(SelectedBorough, SelectedNeighborhood);

        Subways = new ObservableCollection<Pin>(stations.Select(s => new Pin
        {
            Location = s.Location.Coordinate.ToLocation(),
            Label = $"{s.Name} - Routes: {s.Routes}",
            Address = s.LongName,
        }));
    }

    private bool CanShowSubways()
    {
        return !string.IsNullOrEmpty(SelectedBorough) && !string.IsNullOrEmpty(SelectedNeighborhood);
    }
}

public class Neighborhood
{
    public string Name { get; set; }
    public MultiPolygon Geometry { get; set; }
}

public class SubwayStation
{
    public string Name { get; set; }
    public string LongName { get; set; }
    public string Label { get; set; }
    public string Color { get; set; }
    public string Routes { get; set; }
    public Point Location { get; set; }
}
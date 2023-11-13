using CommunityToolkit.Mvvm.ComponentModel;
using NetTopologySuite.Geometries;

namespace SpatialData.Maui;

public partial class MainPageViewModel : ObservableObject
{
    private readonly INewYorkServiceClient client;

    public MainPageViewModel(INewYorkServiceClient client)
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
    private string selectedBorough;

    [ObservableProperty]
    private string? selectedNeighborhood;

    [ObservableProperty]
    private List<string> neighborhoodNames;

    private List<Neighborhood> neighborhoods;

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
}

public class Neighborhood
{
    public string Name { get; set; }
    public MultiPolygon Geometry { get; set; }
}
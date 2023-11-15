using Point = NetTopologySuite.Geometries.Point;

namespace SpatialData.Maui.ApiClient;

public class SubwayStation
{
    public string Name { get; set; }
    public string LongName { get; set; }
    public string Label { get; set; }
    public List<Color> Color { get; set; }
    public string Routes { get; set; }
    public Point Location { get; set; }
    public double Distance { get; set; }
}
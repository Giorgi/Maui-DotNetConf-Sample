using NetTopologySuite.Geometries;

namespace SpatialData.Maui.ApiClient;

public class Neighborhood
{
    public string Name { get; set; }
    public MultiPolygon Geometry { get; set; }
}
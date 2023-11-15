using NetTopologySuite.Geometries;

namespace SpatialData.Maui.Models;

public class Neighborhood
{
    public string Name { get; set; }
    public MultiPolygon Geometry { get; set; }
}
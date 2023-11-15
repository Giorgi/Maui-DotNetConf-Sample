using NetTopologySuite.Geometries;

namespace SpatialData.Api.Models;

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
    public double Distance { get; set; }
}
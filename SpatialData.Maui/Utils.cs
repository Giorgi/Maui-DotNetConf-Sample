namespace SpatialData.Maui;

static class Utils
{
    public static Location ToLocation(this NetTopologySuite.Geometries.Coordinate point)
    {
        return new Location(point.Y, point.X);
    }
}
using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace SpatialData.Api.Models;

public partial class NycNeighborhood
{
    public int Gid { get; set; }

    public string Boroname { get; set; }

    public string Name { get; set; }

    public MultiPolygon? Geom { get; set; }

    public MultiPolygon Location { get; set; }
}

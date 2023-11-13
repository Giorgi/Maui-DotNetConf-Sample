using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace SpatialData.Api.Models;

public partial class NycStreet
{
    public int Gid { get; set; }

    public double? Id { get; set; }

    public string? Name { get; set; }

    public string? Oneway { get; set; }

    public string? Type { get; set; }

    public MultiLineString? Geom { get; set; }
}

using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace SpatialData.Api.Models;

public partial class NycCensusBlock
{
    public int Gid { get; set; }

    public string? Blkid { get; set; }

    public double? PopnTotal { get; set; }

    public double? PopnWhite { get; set; }

    public double? PopnBlack { get; set; }

    public double? PopnNativ { get; set; }

    public double? PopnAsian { get; set; }

    public double? PopnOther { get; set; }

    public string? Boroname { get; set; }

    public MultiPolygon? Geom { get; set; }
}

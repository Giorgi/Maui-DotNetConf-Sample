using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace SpatialData.Api.Models;

public partial class NycSubwayStation
{
    public int Gid { get; set; }

    public decimal? Objectid { get; set; }

    public decimal? Id { get; set; }

    public string Name { get; set; }

    public string AltName { get; set; }

    public string CrossSt { get; set; }

    public string LongName { get; set; }

    public string Label { get; set; }

    public string Borough { get; set; }

    public string Nghbhd { get; set; }

    public string Routes { get; set; }

    public string Transfers { get; set; }

    public string Color { get; set; }

    public string Express { get; set; }

    public string Closed { get; set; }

    public Point? Geom { get; set; }

    public Point Location { get; set; }
}

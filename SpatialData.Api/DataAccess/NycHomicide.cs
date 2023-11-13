using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace SpatialData.Api.Models;

public partial class NycHomicide
{
    public int Gid { get; set; }

    public DateOnly? IncidentD { get; set; }

    public string? Boroname { get; set; }

    public string? NumVictim { get; set; }

    public string? PrimaryMo { get; set; }

    public double? Id { get; set; }

    public string? Weapon { get; set; }

    public string? LightDark { get; set; }

    public double? Year { get; set; }

    public Point? Geom { get; set; }
}

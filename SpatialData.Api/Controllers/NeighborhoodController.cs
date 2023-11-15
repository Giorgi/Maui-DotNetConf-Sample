using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SpatialData.Api.DataAccess;
using SpatialData.Api.Models;

namespace SpatialData.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NeighborhoodController : ControllerBase
    {
        readonly NycContext context;

        public NeighborhoodController(NycContext context)
        {
            this.context = context;
        }

        [HttpGet("Neighborhoods/{borough}")]
        public async Task<List<Neighborhood>> GetNeighborhoods(string borough)
        {
            var items = await context.NycNeighborhoods.Where(n => n.Boroname == borough).
                Select(neighborhood => new Neighborhood
                {
                    Name = neighborhood.Name,
                    Geometry = neighborhood.Location
                }).OrderBy(n => n.Name).
                ToListAsync();

            return items;
        }

        [HttpGet("Subways/{borough}")]
        public async Task<List<SubwayStation>> GetSubways(string borough, string neighborhoodName)
        {
            var neighborhoods = context.NycNeighborhoods.Where(n => n.Boroname == borough && n.Name == neighborhoodName);

            var stations = from neighborhood in neighborhoods
                           from station in context.NycSubwayStations
                           where neighborhood.Location.Covers(station.Location)
                           select new SubwayStation
                           {
                               Location = station.Location,
                               Color = station.Color,
                               Label = station.Label,
                               LongName = station.LongName,
                               Name = station.Name,
                               Routes = station.Routes
                           };

            return await stations.ToListAsync();
        }

        [HttpGet("NearestSubways")]
        public async Task<List<SubwayStation>> GetNearestSubways(double longitude, double latitude, double radius)
        {
            var center = new Point(longitude, latitude);

            var subwaysInRange = await context.NycSubwayStations.Where(station => station.Location.IsWithinDistance(center, radius))
                .Select(station => new SubwayStation
                {
                    Location = station.Location,
                    Color = station.Color,
                    Label = station.Label,
                    LongName = station.LongName,
                    Name = station.Name,
                    Routes = station.Routes,
                    Distance = station.Location.Distance(center)
                })
                .OrderBy(station => station.Distance)
                .ToListAsync();

            return subwaysInRange;
        }
    }
}
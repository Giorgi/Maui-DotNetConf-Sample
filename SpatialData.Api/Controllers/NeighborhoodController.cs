using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpatialData.Api.Models;

namespace SpatialData.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NeighborhoodController : ControllerBase
    {
        NycContext context;
        private readonly ILogger<NeighborhoodController> _logger;

        public NeighborhoodController(ILogger<NeighborhoodController> logger, NycContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet("Neighborhoods/{borough}")]
        public async Task<List<Neighborhood>> GetNeighborhoods(string borough)
        {
            var items = await context.NycNeighborhoods.Where(n => n.Boroname == borough).
                Select(neighborhood => new Neighborhood
                {
                    Name = neighborhood.Name,
                    Geometry = EF.Functions.Transform(neighborhood.Geom, 4326)
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
                                    where neighborhood.Geom.Contains(station.Geom)
                                    select new SubwayStation
                                    {
                                        Location = EF.Functions.Transform(station.Geom, 4326),
                                        Color = station.Color,
                                        Label = station.Label,
                                        LongName = station.LongName,
                                        Name = station.Name,
                                        Routes = station.Routes
                                    };

            return await stations.ToListAsync();
        }
    }
}
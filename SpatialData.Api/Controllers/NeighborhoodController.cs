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

        [HttpGet(Name = "GetNeighborhoods/{borough}")]
        public async Task<List<Neighborhood>> Get(string borough)
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
    }
}
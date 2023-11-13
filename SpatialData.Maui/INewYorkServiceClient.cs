using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace SpatialData.Maui
{
    public interface INewYorkServiceClient
    {
        [Get("/Neighborhood")]
        Task<List<Neighborhood>> GetNeighborhoods(string borough);
    }
}

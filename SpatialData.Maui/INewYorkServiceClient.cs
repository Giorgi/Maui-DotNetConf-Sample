using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SpatialData.Maui.ViewModels;

namespace SpatialData.Maui
{
    public interface INewYorkServiceClient
    {
        [Get("/Neighborhood/Neighborhoods/{borough}")]
        Task<List<Neighborhood>> GetNeighborhoods(string borough);

        [Get("/Neighborhood/Subways/{borough}")]
        Task<List<SubwayStation>> GetSubwayStations(string borough, string neighborhoodName);

        [Get("/Neighborhood/NearestSubways")]
        Task<List<SubwayStation>> GetNearestSubways(double longitude, double latitude, double radius);
    }
}

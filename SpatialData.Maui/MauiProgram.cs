using System.Text.Json;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NetTopologySuite.IO.Converters;
using Refit;
using SpatialData.Maui.Pages;
using SpatialData.Maui.ViewModels;

namespace SpatialData.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiMaps();

            builder.Services.AddTransient<NeighborhoodPage>();
            builder.Services.AddTransient<NeighborhoodPageViewModel>();

            builder.Services.AddTransient<NearestSubwaysPage>();
            builder.Services.AddTransient<NearestSubwaysPageViewModel>();

            builder.Services.AddRefitClient<INewYorkServiceClient>(provider => new RefitSettings(
                new SystemTextJsonContentSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    Converters = { new GeoJsonConverterFactory() },
                    PropertyNameCaseInsensitive = true
                }))).ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri("https://10.0.2.2:5205 ");
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
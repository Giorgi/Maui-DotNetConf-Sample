using System.Text.Json;
using Microsoft.Extensions.Logging;
using NetTopologySuite.IO.Converters;
using Refit;

namespace SpatialData.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiMaps();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ViewModels.MainPageViewModel>();

            builder.Services.AddRefitClient<INewYorkServiceClient>(provider => new RefitSettings(
                new SystemTextJsonContentSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    Converters = { new GeoJsonConverterFactory() },
                    PropertyNameCaseInsensitive = true
                }))).ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri("https://bdcgbfd4-5205.euw.devtunnels.ms");
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
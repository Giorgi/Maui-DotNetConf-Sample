using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpatialData.Maui.ApiClient;

class ColorConverter : JsonConverter<List<Color>>
{
    public override List<Color> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            var colorText = reader.GetString();

            if (string.IsNullOrEmpty(colorText))
            {
                return new List<Color>();
            }

            var colors = colorText.Split('-').Select(colorName =>
            {
                if (colorName == "GREY")
                {
                    colorName = "GRAY";
                }
                return System.Drawing.Color.FromName(colorName);
            }).Select(color => Color.FromInt(color.ToArgb())).ToList();
            return colors;
        }
        catch (Exception)
        {
            reader.Skip();
            return null;
        }
    }

    public override void Write(Utf8JsonWriter writer, List<Color> value, JsonSerializerOptions options)
    {
    }
}
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScheduleCore.Converters;

public class TimeOnlyConverter: JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.FromDateTime(reader.GetDateTime());
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        var isoDate = value.ToString("HH:mm:ss");
        writer.WriteStringValue(isoDate);
    }
}
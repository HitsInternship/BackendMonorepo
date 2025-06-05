using System.Text.Json;
using System.Text.Json.Serialization;

namespace NotificationModule.Application;

public static class JsonOptions
{
    public static readonly JsonSerializerOptions CamelCase = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };
}
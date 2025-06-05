using System.Text.Json;
using System.Text.Json.Nodes;

namespace NotificationModule.Application.Helpers;

public static class JsonHelper
{
    public static JsonObject Serialize<T>(T value)
    {
        return JsonSerializer.SerializeToNode(value, JsonOptions.CamelCase)?.AsObject()
               ?? throw new InvalidOperationException("Failed to serialize to JSON object.");
    }
}
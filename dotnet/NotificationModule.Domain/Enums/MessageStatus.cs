using System.Text.Json.Serialization;

namespace NotificationModule.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MessageStatus
{
    in_progress,
    completed,
    error
}
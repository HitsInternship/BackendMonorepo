using System.Text.Json.Serialization;

namespace NotificationModule.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DeadLineType
{
    practice_diary,
    selection
}
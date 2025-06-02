using System.Text.Json.Serialization;

namespace NotificationModule.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CommentType
{
    selection,
    practice_diary,
    characteristic,
    application
}
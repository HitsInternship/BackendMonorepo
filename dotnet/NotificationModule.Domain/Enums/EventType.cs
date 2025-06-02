using System.Text.Json.Serialization;

namespace NotificationModule.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EventType
{
    changing_practise,
    registration,
    changing_password,
    deadline,
    admission_internship,
    rated_for_practise,
    new_comment
}
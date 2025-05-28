using System.Text.Json.Serialization;

namespace DeanModule.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApplicationStatus
{
    Created,
    UnderConsideration,
    Rejected,
    Accepted
}
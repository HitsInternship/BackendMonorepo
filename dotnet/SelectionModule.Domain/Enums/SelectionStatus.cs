using System.Text.Json.Serialization;

namespace SelectionModule.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SelectionStatus
{
    Inactive, 
    InProgress,
    OffersAccepted
}
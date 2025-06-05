using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using NotificationModule.Domain.Enums;
using Shared.Domain.Entites;

namespace NotificationModule.Domain.Entites;

public class Message : BaseEntity
{
    public string Email { get; set; }
    public EventType EventType { get; set; }
    public string DataJson { get; set; }

    [NotMapped]
    public required JsonObject Data
    {
        get => JsonNode.Parse(DataJson)?.AsObject() ?? throw new InvalidOperationException();
        set => DataJson = value.ToJsonString();
    }

    public MessageStatus MessageStatus { get; set; }
}
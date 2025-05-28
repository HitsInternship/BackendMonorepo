using Shared.Domain.Entites;

namespace SelectionModule.Domain.Entites;

public class PositionEntity : BaseEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }
}
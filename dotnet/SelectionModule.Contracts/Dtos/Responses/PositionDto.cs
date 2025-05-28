using Shared.Contracts.Dtos;

namespace SelectionModule.Contracts.Dtos.Responses;

/// <summary>
/// DTO, представляющее должность (позицию).
/// </summary>
public record PositionDto : BaseDto
{
    /// <summary>
    /// Название должности.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Описание должности (необязательно).
    /// </summary>
    public string? Description { get; set; }
}
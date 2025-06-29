namespace Shared.Contracts.Dtos;

/// <summary>
/// DTO для отображения информации о семестре.
/// </summary>
public record SemesterResponseDto : BaseDto
{
    /// <summary>
    /// Дата начала семестра.
    /// </summary>
    public DateOnly StartDate { get; init; }

    /// <summary>
    /// Дата окончания семестра.
    /// </summary>
    public DateOnly EndDate { get; init; }

    /// <summary>
    /// Описание семестра.
    /// </summary>
    public string? Description { get; init; }
}
using Shared.Contracts.Dtos;
using SelectionModule.Domain.Enums;

namespace SelectionModule.Contracts.Dtos.Responses;

/// <summary>
/// DTO для отображения отбора (selection) с краткой информацией о кандидате.
/// </summary>
public record ListedSelectionDto : BaseDto
{
    /// <summary>
    /// Статус отбора (например: Pending, Approved, Rejected).
    /// </summary>
    public required SelectionStatus SelectionStatus { get; set; }

    /// <summary>
    /// Данные кандидата, связанного с этим отбором.
    /// </summary>
    public required CandidateDto Candidate { get; set; }

    /// <summary>
    /// Данные оффера студента.
    /// </summary>
    public OfferDto? Offer { get; set; }

    /// <summary>
    /// флаг того, что отбор подтвержден
    /// </summary>
    public bool IsConfirmed { get; set; }
}
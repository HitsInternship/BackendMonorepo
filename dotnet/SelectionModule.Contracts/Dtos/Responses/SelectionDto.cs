using SelectionModule.Domain.Enums;
using Shared.Contracts.Dtos;

namespace SelectionModule.Contracts.Dtos.Responses;

/// <summary>
/// DTO, представляющее отбор кандидата на вакансию.
/// </summary>
public record SelectionDto : BaseDto
{
    /// <summary>
    /// Крайний срок завершения отбора.
    /// </summary>
    public required DateOnly DeadLine { get; set; }

    /// <summary>
    /// Текущий статус отбора.
    /// </summary>
    public required SelectionStatus SelectionStatus { get; set; }

    /// <summary>
    /// Информация о кандидате, участвующем в отборе.
    /// </summary>
    public required CandidateDto Candidate { get; set; }

    /// <summary>
    /// Список откликов на вакансии, связанных с этим отбором.
    /// </summary>
    public required List<SelectionVacancyResponseDto> Responses { get; set; }
}

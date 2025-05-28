using SelectionModule.Domain.Enums;
using Shared.Contracts.Dtos;

namespace SelectionModule.Contracts.Dtos.Responses;

/// <summary>
/// DTO, представляющий отклик кандидата на вакансию.
/// </summary>
public record VacancyResponseDto : BaseDto
{
    /// <summary>
    /// Кандидат, откликнувшийся на вакансию.
    /// </summary>
    public CandidateDto Candidate { get; set; }

    /// <summary>
    /// Текущий статус отклика.
    /// </summary>
    public VacancyResponseStatus Status { get; set; }
}
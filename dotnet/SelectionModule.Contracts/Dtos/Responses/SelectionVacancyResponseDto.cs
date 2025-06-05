using CompanyModule.Contracts.DTOs.Responses;
using SelectionModule.Domain.Enums;
using Shared.Contracts.Dtos;

namespace SelectionModule.Contracts.Dtos.Responses;

/// <summary>
/// DTO, представляющее отклик кандидата на вакансию в рамках отбора.
/// </summary>
public record SelectionVacancyResponseDto : BaseDto
{
    /// <summary>
    /// Сокращённая информация о компании, разместившей вакансию.
    /// </summary>
    public required ShortenCompanyDto Company { get; set; }

    /// <summary>
    /// Статус отклика на вакансию.
    /// </summary>
    public required VacancyResponseStatus Status { get; set; }

    /// <summary>
    ///   Позиция, на которую подан отклик.
    /// </summary>
    public required string Position { get; set; }
}
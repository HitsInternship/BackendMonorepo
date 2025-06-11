using CompanyModule.Contracts.DTOs.Responses;
using Shared.Contracts.Dtos;

namespace SelectionModule.Contracts.Dtos.Responses;

/// <summary>
/// DTO для представления полной информации о вакансии.
/// </summary>
public record VacancyDto : BaseDto
{
    /// <summary>
    /// Название вакансии.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание вакансии.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Позиция, к которой относится вакансия.
    /// </summary>
    public PositionDto Position { get; set; }

    /// <summary>
    /// Компания, разместившая вакансию.
    /// </summary>
    public ShortenCompanyDto Company { get; set; }

    /// <summary>
    /// Признак того, что вакансия закрыта.
    /// </summary>
    public bool IsClosed { get; set; }

    /// <summary>
    /// Признак того, что пользователь откликался на вакансию.
    /// </summary>
    public bool HasResponse { get; set; }
}
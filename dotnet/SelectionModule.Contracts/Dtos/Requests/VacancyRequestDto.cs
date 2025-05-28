namespace SelectionModule.Contracts.Dtos.Requests;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTO-запрос для создания или обновления вакансии.
/// </summary>
public record VacancyRequestDto
{
    /// <summary>
    /// Заголовок вакансии.
    /// </summary>
    [Required(ErrorMessage = "Укажите название вакансии")]
    public string Title { get; set; }

    /// <summary>
    /// Описание вакансии.
    /// </summary>
    [Required(ErrorMessage = "Укажите описание вакансии")]
    public string Description { get; set; }

    /// <summary>
    /// ID позиции, к которой относится вакансия.
    /// </summary>
    [Required(ErrorMessage = "PositionId обязателен")]
    public Guid PositionId { get; set; }

    /// <summary>
    /// ID компании, создающей вакансию.
    /// </summary>
    [Required(ErrorMessage = "CompanyId обязателен")]
    public Guid CompanyId { get; set; }
}

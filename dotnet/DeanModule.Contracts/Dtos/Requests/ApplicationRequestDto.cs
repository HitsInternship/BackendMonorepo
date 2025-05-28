using Microsoft.AspNetCore.Http;

namespace DeanModule.Contracts.Dtos.Requests;

/// <summary>
/// DTO для создания или обновления заявки.
/// </summary>
public class ApplicationRequestDto
{
    /// <summary>
    /// Идентификатор студента.
    /// </summary>
    public Guid StudentId { get; set; }

    /// <summary>
    /// Описание заявки.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата подачи заявки.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Идентификатор компании.
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// Идентификатор позиции.
    /// </summary>
    public Guid PositionId { get; set; }

    /// <summary>
    /// Прикреплённый файл.
    /// </summary>
    public IFormFile? File { get; set; }
}
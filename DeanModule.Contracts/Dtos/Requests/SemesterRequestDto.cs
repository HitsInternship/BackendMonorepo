namespace DeanModule.Contracts.Dtos.Requests;


/// <summary>
/// DTO-запрос для создания или обновления семестра.
/// </summary>
public record SemesterRequestDto
{
    /// <summary>
    /// Описание семестра.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата начала семестра.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Дата окончания семестра.
    /// </summary>
    public DateTime EndDate { get; set; }
}
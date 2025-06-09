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
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Дата окончания семестра.
    /// </summary>
    public DateOnly EndDate { get; set; }
}
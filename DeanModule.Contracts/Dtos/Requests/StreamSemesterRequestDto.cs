namespace DeanModule.Contracts.Dtos.Requests;

/// <summary>
/// DTO-запрос для связывания потока с семестром.
/// </summary>
public record StreamSemesterRequestDto
{
    /// <summary>
    /// Идентификатор потока.
    /// </summary>
    public Guid StreamId { get; set; }

    /// <summary>
    /// Идентификатор семестра.
    /// </summary>
    public Guid SemesterId { get; set; }

    /// <summary>
    /// Порядковый номер семестра в рамках потока.
    /// </summary>
    public int Number { get; set; }
}
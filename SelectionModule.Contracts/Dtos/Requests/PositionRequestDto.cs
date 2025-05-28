namespace SelectionModule.Contracts.Dtos.Requests;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTO-запрос для создания или обновления позиции.
/// </summary>
public record PositionRequestDto
{
    /// <summary>
    /// Название позиции.
    /// </summary>
    [Required(ErrorMessage = "Название позиции обязательно")]
    public string Name { get; set; }

    /// <summary>
    /// Описание позиции (необязательно).
    /// </summary>
    public string? Description { get; set; }
}

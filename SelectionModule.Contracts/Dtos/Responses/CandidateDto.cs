using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Dtos;

namespace SelectionModule.Contracts.Dtos.Responses;

/// <summary>
/// DTO, представляющий кандидата.
/// </summary>
public record CandidateDto : BaseDto
{
    /// <summary>
    /// Имя кандидата.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Фамилия кандидата.
    /// </summary>
    [Required]
    public string Surname { get; set; }

    /// <summary>
    /// Отчество кандидата (необязательно).
    /// </summary>
    public string? Middlename { get; set; }

    /// <summary>
    /// Email кандидата.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Номер телефона кандидата.
    /// </summary>
    [Required]
    public string Phone { get; set; }

    /// <summary>
    /// Номер группы, к которой относится кандидат.
    /// </summary>
    public int GroupNumber { get; set; }
}
using CompanyModule.Contracts.DTOs.Responses;
using DeanModule.Domain.Enums;
using SelectionModule.Contracts.Dtos.Responses;
using Shared.Contracts.Dtos;
using StudentModule.Contracts.DTOs;

namespace DeanModule.Contracts.Dtos.Responses;

/// <summary>
/// Упрощённая информация о заявке.
/// </summary>
public record ListedApplicationResponseDto : BaseDto
{
    /// <summary>
    /// Дата подачи заявки.
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    /// Статус заявки.
    /// </summary>
    public ApplicationStatus Status { get; init; }

    public StudentDto Student { get; set; }

    public CompanyResponse Company { get; set; }

    public PositionDto Position { get; set; }
}
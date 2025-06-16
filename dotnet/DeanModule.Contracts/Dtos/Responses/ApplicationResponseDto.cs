using CompanyModule.Contracts.DTOs.Responses;
using DeanModule.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelectionModule.Contracts.Dtos.Responses;
using StudentModule.Contracts.DTOs;

namespace DeanModule.Contracts.Dtos.Responses;

/// <summary>
/// Ответ по заявке студента.
/// </summary>
public record ApplicationResponseDto
{
    /// <summary>
    /// Описание заявки.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Дата подачи заявки.
    /// </summary>
    public DateOnly Date { get; init; }

    /// <summary>
    /// URL на прикреплённый документ (если имеется).
    /// </summary>
    public string? DocumentUrl { get; init; }

    /// <summary>
    /// Текущий статус заявки.
    /// </summary>
    public ApplicationStatus Status { get; init; }

    public StudentDto Student { get; set; }

    public CompanyResponse NewCompany { get; set; }

    public PositionDto NewPosition { get; set; }
    
    public CompanyResponse OldCompany { get; set; }

    public PositionDto OldPosition { get; set; }
}
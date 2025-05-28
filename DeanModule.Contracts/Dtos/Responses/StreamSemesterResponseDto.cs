using Shared.Contracts.Dtos;
using StudentModule.Contracts.DTOs;

namespace DeanModule.Contracts.Dtos.Responses;

/// <summary>
/// DTO для представления связи между потоком и семестром.
/// </summary>
public record StreamSemesterResponseDto : BaseDto
{
    public StreamDto Stream { get; set; }

    /// <summary>
    /// Порядковый номер семестра в рамках потока.
    /// </summary>
    public int Number { get; init; }

    /// <summary>
    /// Информация о семестре.
    /// </summary>
    public required SemesterResponseDto Semester { get; init; }
}
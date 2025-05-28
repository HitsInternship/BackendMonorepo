using Shared.Contracts.Dtos;

namespace DeanModule.Contracts.Dtos.Responses;

/// <summary>
/// DTO со списком заявок и параметрами пагинации.
/// </summary>
public class ApplicationsDto(List<ListedApplicationResponseDto> applications, int size, int count, int current)
{
    /// <summary>
    /// Коллекция заявок.
    /// </summary>
    public List<ListedApplicationResponseDto> Applications { get; init; } = applications;

    /// <summary>
    /// Информация о пагинации.
    /// </summary>
    public Pagination Pagination { get; init; } = new(size, count, current);
}
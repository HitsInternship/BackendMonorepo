using Shared.Contracts.Dtos;

namespace CompanyModule.Contracts.DTOs.Responses;

public record ShortenCompanyDto : BaseDto
{
    public string Name { get; set; }
}
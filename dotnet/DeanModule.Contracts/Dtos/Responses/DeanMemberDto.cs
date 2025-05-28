using Shared.Contracts.Dtos;

namespace DeanModule.Contracts.Dtos.Responses;

public record DeanMemberDto : BaseDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
}
using Shared.Contracts.Dtos;
using Shared.Domain.Entites;
using StudentModule.Contracts.DTOs;

namespace SelectionModule.Contracts.Dtos.Responses;

public class GlobalSelectionDto : BaseEntity
{
    public SemesterResponseDto Semester { get; set; }
    public StreamDto Stream { get; set; }
}
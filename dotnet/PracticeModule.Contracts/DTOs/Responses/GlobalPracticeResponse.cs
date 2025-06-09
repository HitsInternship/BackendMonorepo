using PracticeModule.Domain.Enum;

namespace PracticeModule.Contracts.DTOs.Responses;

public class GlobalPracticeResponse
{
    public Guid id { get; set; }
    public Guid semesterId { get; set; }
    public Guid streamId { get; set; }
    public GlobalPracticeType practiceType { get; set; }
}
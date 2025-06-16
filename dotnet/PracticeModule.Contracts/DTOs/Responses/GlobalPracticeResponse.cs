using Microsoft.AspNetCore.Http;
using PracticeModule.Domain.Enum;

namespace PracticeModule.Contracts.DTOs.Responses;

public class GlobalPracticeResponse
{
    public Guid id { get; set; }
    public Guid semesterId { get; set; }
    public Guid streamId { get; set; }
    public GlobalPracticeType practiceType { get; set; }

    public Guid diaryPatternDocumentId { get; set; }
    public Guid characteristicsPatternDocumentId { get; set; }
}
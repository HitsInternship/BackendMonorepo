using DeanModule.Domain.Entities;
using Microsoft.AspNetCore.Http;
using PracticeModule.Domain.Enum;
using StudentModule.Domain.Entities;

namespace PracticeModule.Contracts.DTOs.Responses;

public class SemesterPracticeResponse
{
    public Guid semesterId { get; set; }
    public DateOnly semesterStartDate { get; set; }
    public DateOnly semesterEndDate { get; set; }
    public List<GlobalPracticeResponse> globalPractices { get; set; }
}

public class GlobalPracticeResponse
{
    public Guid id { get; set; }
    public Guid semesterId { get; set; }
    public Guid streamId { get; set; }
    public string? streamNumber { get; set; }
    public GlobalPracticeType practiceType { get; set; }

    public Guid diaryPatternDocumentId { get; set; }
    public Guid characteristicsPatternDocumentId { get; set; }
}
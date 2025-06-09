using System.ComponentModel.DataAnnotations;
using PracticeModule.Domain.Enum;
using Shared.Domain.Entites;

namespace PracticeModule.Domain.Entity;

public class GlobalPractice : BaseEntity
{
    public GlobalPracticeType PracticeType { get; set; }
    public Guid SemesterId { get; set; }
    public Guid StreamId { get; set; }

    public Guid DiaryPatternDocumentId { get; set; }
    public Guid CharacteristicsPatternDocumentId { get; set; }

    public List<Practice> Practices { get; set; }
}
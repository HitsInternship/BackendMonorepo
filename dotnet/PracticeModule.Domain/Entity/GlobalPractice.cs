using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeanModule.Domain.Entities;
using PracticeModule.Domain.Enum;
using Shared.Domain.Entites;
using StudentModule.Domain.Entities;

namespace PracticeModule.Domain.Entity;

public class GlobalPractice : BaseEntity
{
    public GlobalPracticeType PracticeType { get; set; }
    public Guid SemesterId { get; set; }
    [NotMapped]
    public SemesterEntity Semester { get; set; }
    public Guid StreamId { get; set; }
    [NotMapped]
    public StreamEntity Stream { get; set; }

    public Guid DiaryPatternDocumentId { get; set; }
    public Guid CharacteristicsPatternDocumentId { get; set; }

    public List<Practice> Practices { get; set; }
}
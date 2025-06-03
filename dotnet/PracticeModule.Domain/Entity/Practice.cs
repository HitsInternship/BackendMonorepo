using System.ComponentModel.DataAnnotations;
using Shared.Domain.Entites;

namespace PracticeModule.Domain.Entity;

public class Practice : BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid SemesterId { get; set; }
    public Guid PositionId { get; set; }
    public int Mark { get; set; }
    public List<StudentPracticeCharacteristic> StudentPracticeCharacteristics { get; set; }
    public List<PracticeDiary> PracticeDiaries { get; set; }
    public PracticeType PracticeType { get; set; }
    public bool IsDeleted { get; set; } =  false;
}

public enum PracticeType
{
    Technological
}
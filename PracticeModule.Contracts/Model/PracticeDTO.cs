using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.Model;

public class PracticeDTO
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid SemesterId { get; set; }
    public Guid PositionId { get; set; }
    public int Mark { get; set; }
    public List<StudentPracticeCharacteristic> StudentPracticeCharacteristics { get; set; }
    public List<PracticeDiary> PracticeDiaries { get; set; }
    public PracticeType PracticeType { get; set; }
}
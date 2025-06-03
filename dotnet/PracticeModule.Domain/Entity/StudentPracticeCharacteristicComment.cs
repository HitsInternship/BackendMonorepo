using System.ComponentModel.DataAnnotations;

namespace PracticeModule.Domain.Entity;

public class StudentPracticeCharacteristicComment
{
    [Key]
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public Guid? PracticeCharacteristicId { get; set; }
    public StudentPracticeCharacteristic? PracticeCharacteristic { get; set; }
}
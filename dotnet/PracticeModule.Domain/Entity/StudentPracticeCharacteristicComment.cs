using Shared.Domain.Entites;
using System.ComponentModel.DataAnnotations;

namespace PracticeModule.Domain.Entity;

public class StudentPracticeCharacteristicComment : BaseEntity
{
    public string Comment { get; set; }
    public Guid? PracticeCharacteristicId { get; set; }
    public StudentPracticeCharacteristic? PracticeCharacteristic { get; set; }
}
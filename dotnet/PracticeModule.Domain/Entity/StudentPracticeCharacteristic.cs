using Shared.Domain.Entites;
using System.ComponentModel.DataAnnotations;

namespace PracticeModule.Domain.Entity;

public class StudentPracticeCharacteristic : BaseEntity
{
    public Guid DocumentId { get; set; }
    public Practice Practice { get; set; }
    public Guid PracticeId { get; set; }
    public List<StudentPracticeCharacteristicComment>? PracticeComment { get; set; }
    
}
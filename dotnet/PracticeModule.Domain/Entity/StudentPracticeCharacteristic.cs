using System.ComponentModel.DataAnnotations;

namespace PracticeModule.Domain.Entity;

public class StudentPracticeCharacteristic
{
    [Key]
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public Practice Practice { get; set; }
    public Guid PracticeId { get; set; }
    public List<StudentPracticeCharacteristicComment>? PracticeComment { get; set; }
    
}
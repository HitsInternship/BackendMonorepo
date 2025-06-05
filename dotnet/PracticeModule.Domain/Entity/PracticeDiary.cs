using System.ComponentModel.DataAnnotations;

namespace PracticeModule.Domain.Entity;

public class PracticeDiary
{
    [Key]
    public Guid Id { get; set; }
    public Practice Practice { get; set; }
    public Guid PracticeId { get; set; }
    public Guid DocumentId { get; set; }
    public List<PracticeDiaryComment>? Comment { get; set; }
}
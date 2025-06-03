using System.ComponentModel.DataAnnotations;

namespace PracticeModule.Domain.Entity;

public class PracticeDiaryComment
{
    [Key]
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public Guid? DiaryId { get; set; }
    public PracticeDiary? Diary { get; set; }
    
}

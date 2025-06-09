using Shared.Domain.Entites;
using System.ComponentModel.DataAnnotations;

namespace PracticeModule.Domain.Entity;

public class PracticeDiary : BaseEntity
{
    public Practice Practice { get; set; }
    public Guid PracticeId { get; set; }
    public Guid DocumentId { get; set; }
    public List<PracticeDiaryComment>? Comment { get; set; }
}
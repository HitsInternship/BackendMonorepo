using Shared.Domain.Entites;
using System.ComponentModel.DataAnnotations;

namespace PracticeModule.Domain.Entity;

public class PracticeDiaryComment : BaseEntity
{

    public string Comment { get; set; }
    public Guid? DiaryId { get; set; }
}

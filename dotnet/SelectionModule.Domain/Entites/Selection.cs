using System.ComponentModel.DataAnnotations.Schema;
using SelectionModule.Domain.Enums;
using Shared.Domain.Entites;

namespace SelectionModule.Domain.Entites;

public class SelectionEntity : BaseEntity
{
    public required DateOnly DeadLine { get; set; }

    public required Guid CandidateId { get; set; }

    [ForeignKey("CandidateId")]
    public required CandidateEntity Candidate { get; set; }

    public required SelectionStatus SelectionStatus { get; set; }
    
    public Guid? Offer { get; set; }

    public ICollection<SelectionCommentEntity> Comments { get; set; } = [];
}

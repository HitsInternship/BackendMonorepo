using SelectionModule.Domain.Enums;
using Shared.Domain.Entites;

namespace SelectionModule.Domain.Entites;

public class VacancyResponseEntity : BaseEntity
{
    public Guid VacancyId { get; set; }

    public VacancyEntity Vacancy { get; set; } = null!;

    public Guid CandidateId { get; set; }

    public CandidateEntity Candidate { get; set; } = null!;
    
    public VacancyResponseStatus Status { get; set; }

    public ICollection<VacancyResponseCommentEntity> Comments { get; set; } = [];
}

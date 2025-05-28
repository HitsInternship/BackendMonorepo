using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Entites;

namespace SelectionModule.Domain.Entites;

public class CandidateEntity : BaseEntity
{
    public Guid UserId { get; set; }

    public SelectionEntity? Selection { get; set; }

    public Guid StudentId { get; set; }

    public ICollection<VacancyResponseEntity> Responses { get; set; } = [];
}

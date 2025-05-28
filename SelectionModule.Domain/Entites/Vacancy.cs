using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Entites;

namespace SelectionModule.Domain.Entites;

public class VacancyEntity : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }
    
    [ForeignKey("PositionId")]
    public required PositionEntity Position { get; set; }
    
    public required Guid PositionId { get; set; }
    
    public required Guid CompanyId { get; set; }
    
    public bool IsClosed { get; set; } = false;

    public ICollection<VacancyResponseEntity> Responses { get; set; } = [];
}
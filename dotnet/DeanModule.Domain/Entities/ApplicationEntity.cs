using DeanModule.Domain.Enums;
using Shared.Domain.Entites;

namespace DeanModule.Domain.Entities;

public class ApplicationEntity : BaseEntity
{
    public Guid StudentId { get; set; }
    public string? Description { get; set; }
    public DateOnly Date { get; set; }
    public Guid CompanyId { get; set; }
    public Guid PositionId { get; set; }
    public Guid? DocumentId { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Created;
    public ICollection<ApplicationComment> Comments { get; set; } = [];
}
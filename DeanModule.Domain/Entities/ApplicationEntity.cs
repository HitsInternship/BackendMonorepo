using DeanModule.Domain.Enums;
using Shared.Domain.Entites;

namespace DeanModule.Domain.Entities;

public class ApplicationEntity : BaseEntity
{
    public Guid StudentId { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public Guid CompanyId { get; set; }
    public Guid PositionId { get; set; }
    public string? DocumentUrl { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Created;
}
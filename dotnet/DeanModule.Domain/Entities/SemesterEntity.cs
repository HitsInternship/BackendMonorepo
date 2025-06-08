using Shared.Domain.Entites;

namespace DeanModule.Domain.Entities;

public class SemesterEntity : BaseEntity
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? Description { get; set; }
}
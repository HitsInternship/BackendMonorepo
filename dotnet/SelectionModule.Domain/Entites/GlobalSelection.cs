using System.ComponentModel.DataAnnotations;
using Shared.Domain.Entites;

namespace SelectionModule.Domain.Entites;

public class GlobalSelection : BaseEntity
{
    [Required]
    public Guid StreamId { get; set; }
    
    [Required]
    public Guid SemesterId { get; set; }
    
    [Required]
    public DateOnly StartDate { get; set; }
    
    [Required]
    public DateOnly EndDate { get; set; }
    
    public ICollection<SelectionEntity> Selections { get; set; } = [];
}
using System.ComponentModel.DataAnnotations;

namespace Shared.Contracts.Dtos;

public abstract record BaseDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public bool IsDeleted { get; set; }
}
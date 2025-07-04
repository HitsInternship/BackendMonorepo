using System.ComponentModel.DataAnnotations;
using SelectionModule.Domain.Enums;

namespace SelectionModule.Contracts.Dtos.Requests;

public record CommentRequestDto
{
    [Required] public required string Content { get; set; }
    public SelectionStatus? SelectionStatus { get; set; }
    
    public Guid? CompanyId { get; set; }
    
    public Guid? PositionId { get; set; }
    public List<Guid>? SelectedUsers { get; set; }
}
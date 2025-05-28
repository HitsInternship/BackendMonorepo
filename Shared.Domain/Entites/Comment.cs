namespace Shared.Domain.Entites;

public class Comment : BaseEntity
{
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public Guid ParentId { get; set; }
}
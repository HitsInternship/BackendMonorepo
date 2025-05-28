using Shared.Domain.Entites;

namespace SelectionModule.Domain.Entites;

public class SelectionCommentEntity : Comment
{
    public SelectionEntity Selection { get; set; }
}
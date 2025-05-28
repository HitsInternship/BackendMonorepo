using Shared.Domain.Entites;

namespace SelectionModule.Domain.Entites;

public class VacancyResponseCommentEntity : Comment
{
    public VacancyResponseEntity VacancyResponse { get; set; }
}
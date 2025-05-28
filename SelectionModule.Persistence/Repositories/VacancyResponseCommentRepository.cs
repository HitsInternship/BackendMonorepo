using SelectionModule.Contracts.Repositories;
using SelectionModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace SelectionModule.Persistence.Repositories;

public class VacancyResponseCommentRepository(SelectionDbContext context)
    : BaseEntityRepository<Domain.Entites.VacancyResponseCommentEntity>(context), IVacancyResponseCommentRepository
{
}
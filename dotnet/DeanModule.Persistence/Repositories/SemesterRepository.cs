using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using DeanModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace DeanModule.Persistence.Repositories;

public class SemesterRepository(DeanModuleDbContext context)
    : BaseEntityRepository<SemesterEntity>(context), ISemesterRepository;
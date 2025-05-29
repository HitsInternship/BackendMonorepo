using CompanyModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Commands.Vacancy;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Vacancy;

public class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand, Unit>
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly ICuratorRepository _companyPersonRepository;

    public DeleteVacancyCommandHandler(IVacancyRepository vacancyRepository,
        ICuratorRepository companyPersonRepository)
    {
        _vacancyRepository = vacancyRepository;
        _companyPersonRepository = companyPersonRepository;
    }

    public async Task<Unit> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
    {
        if (!await _vacancyRepository.CheckIfExistsAsync(request.VacancyId))
            throw new NotFound("Vacancy not found");

        var vacancy = await _vacancyRepository.GetByIdAsync(request.VacancyId);

        if (request.UserId.HasValue)
        {
            if (!await _companyPersonRepository.CheckIfUserIsCurator(vacancy.CompanyId, request.UserId.Value))
                throw new Forbidden("You are not allowed to delete this vacancy");
        }
        
        if (request.ToArchive) await _vacancyRepository.SoftDeleteAsync(vacancy.Id);
        else await _vacancyRepository.DeleteAsync(vacancy);

        return Unit.Value;
    }
}
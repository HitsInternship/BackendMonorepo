using CompanyModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Commands.Vacancy;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Vacancy;

public class CloseVacancyCommandHandler : IRequestHandler<CloseVacancyCommand, Unit>
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly ICuratorRepository _companyPersonRepository;

    public CloseVacancyCommandHandler(IVacancyRepository vacancyRepository, ICuratorRepository companyPersonRepository)
    {
        _vacancyRepository = vacancyRepository;
        _companyPersonRepository = companyPersonRepository;
    }

    public async Task<Unit> Handle(CloseVacancyCommand request, CancellationToken cancellationToken)
    {
        if(!await _vacancyRepository.CheckIfExistsAsync(request.VacancyId))
            throw new NotFound("Vacancy not found");
        
        var vacancy = await _vacancyRepository.GetByIdAsync(request.VacancyId);

        if (!await _companyPersonRepository.CheckIfUserIsCurator(vacancy.CompanyId, request.UserId))
            throw new Forbidden("You cannot close this vacancy");
        
        vacancy.IsClosed = true;
        
        await _vacancyRepository.UpdateAsync(vacancy);
        
        return Unit.Value;
    }
}
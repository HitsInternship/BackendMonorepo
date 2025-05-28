using CompanyModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Commands.Vacancy;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Vacancy;

public class UpdateVacancyCommandHandler : IRequestHandler<UpdateVacancyCommand, Unit>
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyPersonRepository _companyPersonRepository;

    public UpdateVacancyCommandHandler(IVacancyRepository vacancyRepository, ICompanyRepository companyRepository,
        IPositionRepository positionRepository, ICompanyPersonRepository companyPersonRepository)
    {
        _vacancyRepository = vacancyRepository;
        _companyRepository = companyRepository;
        _positionRepository = positionRepository;
        _companyPersonRepository = companyPersonRepository;
    }

    public async Task<Unit> Handle(UpdateVacancyCommand request, CancellationToken cancellationToken)
    {
        if (!await _vacancyRepository.CheckIfExistsAsync(request.VacancyId))
            throw new NotFound("Vacancy not found");

        if (!await _companyRepository.CheckIfExistsAsync(request.VacancyRequestDto.CompanyId))
            throw new BadRequest("Invalid companyId");

        if (!await _positionRepository.CheckIfExistsAsync(request.VacancyRequestDto.PositionId))
            throw new BadRequest("Invalid positionId");

        var vacancy = await _vacancyRepository.GetByIdAsync(request.VacancyId);

        if (request.UserId.HasValue)
        {
            if (!await _companyPersonRepository.CheckIfUserIsCompanyPerson(request.VacancyRequestDto.CompanyId,
                    request.UserId.Value))
                throw new Forbidden("You cannot create a vacancy from this company");

            if (!await _companyPersonRepository.CheckIfUserIsCompanyPerson(vacancy.CompanyId,
                    request.UserId.Value))
                throw new Forbidden("You cannot create a vacancy from this company");
        }


        vacancy.PositionId = request.VacancyRequestDto.PositionId;
        vacancy.CompanyId = request.VacancyRequestDto.CompanyId;
        vacancy.Title = request.VacancyRequestDto.Title;
        vacancy.Description = request.VacancyRequestDto.Description;
        vacancy.Position = await _positionRepository.GetByIdAsync(request.VacancyRequestDto.PositionId);

        await _vacancyRepository.UpdateAsync(vacancy);

        return Unit.Value;
    }
}
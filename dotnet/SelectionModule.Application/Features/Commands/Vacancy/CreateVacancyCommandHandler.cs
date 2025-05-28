using CompanyModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Commands.Vacancy;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Vacancy;

public class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, Unit>
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyPersonRepository _companyPersonRepository;

    public CreateVacancyCommandHandler(IVacancyRepository vacancyRepository, IPositionRepository positionRepository,
        ICompanyRepository companyRepository, ICompanyPersonRepository companyPersonRepository)
    {
        _vacancyRepository = vacancyRepository;
        _positionRepository = positionRepository;
        _companyRepository = companyRepository;
        _companyPersonRepository = companyPersonRepository;
    }

    public async Task<Unit> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
    {
        if (!await _companyRepository.CheckIfExistsAsync(request.VacancyRequestDto.CompanyId))
            throw new BadRequest("Invalid companyId");

        if (!await _positionRepository.CheckIfExistsAsync(request.VacancyRequestDto.PositionId))
            throw new BadRequest("Invalid positionId");

        if (request.UserId.HasValue)
        {
            if (!await _companyPersonRepository.CheckIfUserIsCompanyPerson(request.VacancyRequestDto.CompanyId,
                    request.UserId.Value))
                throw new Forbidden("You cannot create a vacancy from this company");
        }


        await _vacancyRepository.AddAsync(new VacancyEntity
        {
            Title = request.VacancyRequestDto.Title,
            Description = request.VacancyRequestDto.Description,
            Position = await _positionRepository.GetByIdAsync(request.VacancyRequestDto.PositionId),
            PositionId = request.VacancyRequestDto.PositionId,
            CompanyId = request.VacancyRequestDto.CompanyId,
        });

        return Unit.Value;
    }
}
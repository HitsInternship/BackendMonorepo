using MediatR;
using SelectionModule.Contracts.Commands.VacancyResponse;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Enums;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.VacancyResponse;

public class CreateVacancyResponseCommandHandler : IRequestHandler<CreateVacancyResponseCommand, Unit>
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;
    private readonly ICandidateRepository _candidateRepository;

    public CreateVacancyResponseCommandHandler(IVacancyRepository vacancyRepository,
        ICandidateRepository candidateRepository, IVacancyResponseRepository vacancyResponseRepository)
    {
        _vacancyRepository = vacancyRepository;
        _candidateRepository = candidateRepository;
        _vacancyResponseRepository = vacancyResponseRepository;
    }

    public async Task<Unit> Handle(CreateVacancyResponseCommand request, CancellationToken cancellationToken)
    {
        if (!await _vacancyRepository.CheckIfExistsAsync(request.VacancyId))
            throw new BadRequest("Vacancy not found");

        var vacancy = await _vacancyRepository.GetByIdAsync(request.VacancyId);

        if (vacancy.IsClosed) throw new BadRequest("Vacancy is closed");

        var candidate = await _candidateRepository.GetCandidateByUsrIdAsync(request.UserId) ??
                        throw new Forbidden("You are not a candidate");

        var vacancyResponse = new Domain.Entites.VacancyResponseEntity()
        {
            VacancyId = request.VacancyId,
            CandidateId = candidate.Id,
            Candidate = candidate,
            Vacancy = vacancy,
            Status = VacancyResponseStatus.Responding,
        };


        vacancy.Responses.Add(vacancyResponse);

        await _vacancyRepository.UpdateAsync(vacancy);
        await _vacancyResponseRepository.AddAsync(vacancyResponse);

        return Unit.Value;
    }
}
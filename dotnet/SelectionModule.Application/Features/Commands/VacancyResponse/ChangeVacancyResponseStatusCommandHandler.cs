using MediatR;
using SelectionModule.Contracts.Commands.VacancyResponse;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Enums;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.VacancyResponse;

public class ChangeVacancyResponseStatusCommandHandler : IRequestHandler<ChangeVacancyResponseStatusCommand, Unit>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;

    public ChangeVacancyResponseStatusCommandHandler(IVacancyResponseRepository vacancyResponseRepository,
        ICandidateRepository candidateRepository)
    {
        _vacancyResponseRepository = vacancyResponseRepository;
        _candidateRepository = candidateRepository;
    }


    public async Task<Unit> Handle(ChangeVacancyResponseStatusCommand request, CancellationToken cancellationToken)
    {
        if (!await _vacancyResponseRepository.CheckIfExistsAsync(request.VacancyResponseId))
            throw new NotFound("VacancyResponse not found");

        var vacancyResponse = await _vacancyResponseRepository.GetByIdAsync(request.VacancyResponseId);

        if (vacancyResponse.Candidate.UserId != request.UserId)
            throw new Forbidden("You cannot change the status of this vacancy response");

        vacancyResponse.Status = request.Status;

        await _vacancyResponseRepository.UpdateAsync(vacancyResponse);

        var candidate = await _candidateRepository.GetCandidateByUserIdAsync(request.UserId) ??
                        throw new BadRequest("There is no candidate for this user");

        if (request.Status == VacancyResponseStatus.GotOffer)
        {
            if (candidate.Selection != null) candidate.Selection.Offer = vacancyResponse.Vacancy.Id;
        }

        await _candidateRepository.UpdateAsync(candidate);

        return Unit.Value;
    }
}
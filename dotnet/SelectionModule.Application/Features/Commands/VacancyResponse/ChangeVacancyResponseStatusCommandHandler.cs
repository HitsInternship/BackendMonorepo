using MediatR;
using SelectionModule.Contracts.Commands.VacancyResponse;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.VacancyResponse;

public class ChangeVacancyResponseStatusCommandHandler : IRequestHandler<ChangeVacancyResponseStatusCommand, Unit>
{
    private readonly IVacancyResponseRepository _vacancyResponseRepository;

    public ChangeVacancyResponseStatusCommandHandler(IVacancyResponseRepository vacancyResponseRepository)
    {
        _vacancyResponseRepository = vacancyResponseRepository;
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

        return Unit.Value;
    }
}
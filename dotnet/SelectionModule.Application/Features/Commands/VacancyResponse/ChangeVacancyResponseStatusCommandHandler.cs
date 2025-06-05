using MediatR;
using SelectionModule.Contracts.Commands.VacancyResponse;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Enums;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.VacancyResponse;

public class ChangeVacancyResponseStatusCommandHandler : IRequestHandler<ChangeVacancyResponseStatusCommand, Unit>
{
    private readonly IVacancyResponseRepository _vacancyResponseRepository;
    private readonly ISelectionRepository _selectionRepository;

    public ChangeVacancyResponseStatusCommandHandler(IVacancyResponseRepository vacancyResponseRepository,
        ISelectionRepository selectionRepository)
    {
        _vacancyResponseRepository = vacancyResponseRepository;
        _selectionRepository = selectionRepository;
    }


    public async Task<Unit> Handle(ChangeVacancyResponseStatusCommand request, CancellationToken cancellationToken)
    {
        if (!await _vacancyResponseRepository.CheckIfExistsAsync(request.VacancyResponseId))
            throw new NotFound("VacancyResponse not found");

        var vacancyResponse = await _vacancyResponseRepository.GetByIdAsync(request.VacancyResponseId);

        if (vacancyResponse.Candidate.UserId != request.UserId)
            throw new Forbidden("You cannot change the status of this vacancy response");

        vacancyResponse.Status = request.Status;

        if (vacancyResponse.Status == VacancyResponseStatus.OfferAccepted)
        {
            var selection = vacancyResponse.Candidate.Selection;

            if (selection != null)
            {
                selection.SelectionStatus = SelectionStatus.OffersAccepted;
                selection.Offer = vacancyResponse.Id;
                await _selectionRepository.UpdateAsync(selection);
            }
        }

        await _vacancyResponseRepository.UpdateAsync(vacancyResponse);

        return Unit.Value;
    }
}
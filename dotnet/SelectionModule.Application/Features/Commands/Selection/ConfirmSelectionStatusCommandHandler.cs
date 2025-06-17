using CompanyModule.Contracts.Repositories;
using MediatR;
using NotificationModule.Contracts.Commands;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using SelectionModule.Domain.Enums;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Commands.Selection;

public class ConfirmSelectionStatusCommandHandler : IRequestHandler<ConfirmSelectionStatusCommand, Unit>
{
    private readonly ISender _mediator;
    private readonly ISelectionRepository _selectionRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IUserRepository _userRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;

    public ConfirmSelectionStatusCommandHandler(ISelectionRepository selectionRepository,
        ICandidateRepository candidateRepository, IVacancyResponseRepository vacancyResponseRepository,
        ISender mediator, IUserRepository userRepository)
    {
        _selectionRepository = selectionRepository;
        _candidateRepository = candidateRepository;
        _vacancyResponseRepository = vacancyResponseRepository;
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ConfirmSelectionStatusCommand request, CancellationToken cancellationToken)
    {
        if (!await _selectionRepository.CheckIfExistsAsync(request.SelectionId))
            throw new NotFound("Selection doesn't exist");

        var selection = await _selectionRepository.GetByIdAsync(request.SelectionId);

        if (selection.SelectionStatus != SelectionStatus.OffersAccepted)
            throw new BadRequest("You cannot confirm selection with this status ");

        if (!selection.Offer.HasValue) throw new BadRequest("You cannot confirm selection");

        selection.IsConfirmed = true;

        await _selectionRepository.UpdateAsync(selection);

        await SendAdmissionInternshipMessageAsync(selection);

        return Unit.Value;
    }

    private async Task SendAdmissionInternshipMessageAsync(SelectionEntity selection)
    {
        var user = await _userRepository.GetByIdAsync(selection.Candidate.UserId);

        var vacancy = await _mediator.Send(new GetVacancyQuery(selection.Offer!.Value, Guid.Empty, []));

        await _mediator.Send(
            new SendAdmissionInternshipMessageCommand(user.Email, vacancy.Company.Name, vacancy.Position.Name));
    }
}
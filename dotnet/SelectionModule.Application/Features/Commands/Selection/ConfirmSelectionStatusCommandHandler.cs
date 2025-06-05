using CompanyModule.Contracts.Repositories;
using MediatR;
using NotificationModule.Contracts.Commands;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Enums;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Commands.Selection;

public class ConfirmSelectionStatusCommandHandler : IRequestHandler<ConfirmSelectionStatusCommand, Unit>
{
    private readonly ISender _sender;
    private readonly ISelectionRepository _selectionRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;

    public ConfirmSelectionStatusCommandHandler(ISelectionRepository selectionRepository,
        ICandidateRepository candidateRepository, IVacancyResponseRepository vacancyResponseRepository, ISender sender,
        IUserRepository userRepository, ICompanyRepository companyRepository)
    {
        _selectionRepository = selectionRepository;
        _candidateRepository = candidateRepository;
        _vacancyResponseRepository = vacancyResponseRepository;
        _sender = sender;
        _userRepository = userRepository;
        _companyRepository = companyRepository;
    }

    public async Task<Unit> Handle(ConfirmSelectionStatusCommand request, CancellationToken cancellationToken)
    {
        if (!await _selectionRepository.CheckIfExistsAsync(request.SelectionId))
            throw new NotFound("Selection doesn't exist");

        var selection = await _selectionRepository.GetByIdAsync(request.SelectionId);

        if (selection.SelectionStatus != SelectionStatus.OffersAccepted)
            throw new BadRequest("You cannot confirm selection with this status ");

        //todo: create practice

        await _selectionRepository.SoftDeleteAsync(selection.Id);
        await _candidateRepository.SoftDeleteAsync(selection.CandidateId);
        await _vacancyResponseRepository.SoftDeleteByCandidateAsync(selection.CandidateId);

        var user = await _userRepository.GetByIdAsync(selection.Candidate.UserId);
        if (selection.Offer != null)
        {
            var offer = await _vacancyResponseRepository.GetByIdAsync(selection.Offer.Value);

            var company = await _companyRepository.GetByIdAsync(offer.Vacancy.CompanyId);

            await _sender.Send(new SendAdmissionInternshipMessageCommand(user.Email, company.Name, offer.Vacancy.Title),
                cancellationToken);
        }

        return Unit.Value;
    }
}
using MediatR;
using NotificationModule.Contracts.Commands;
using NotificationModule.Domain.Enums;
using SelectionModule.Contracts.Commands.VacancyResponseComment;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Commands.VacancyResponseComment;

public class CreateVacancyResponseCommentCommandHandler : IRequestHandler<CreateVacancyResponseCommentCommand, Unit>
{
    private readonly ISender _mediator;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IUserRepository _userRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;
    private readonly IVacancyResponseCommentRepository _vacancyResponseCommentRepository;

    public CreateVacancyResponseCommentCommandHandler(IVacancyResponseRepository vacancyResponseRepository,
        IVacancyResponseCommentRepository vacancyResponseCommentRepository, IUserRepository userRepository,
        ISender mediator, ICandidateRepository candidateRepository)
    {
        _vacancyResponseRepository = vacancyResponseRepository;
        _vacancyResponseCommentRepository = vacancyResponseCommentRepository;
        _userRepository = userRepository;
        _mediator = mediator;
        _candidateRepository = candidateRepository;
    }

    public async Task<Unit> Handle(CreateVacancyResponseCommentCommand request, CancellationToken cancellationToken)
    {
        if (!await _vacancyResponseRepository.CheckIfExistsAsync(request.VacancyResponseId))
            throw new NotFound("Selection not found");

        var vacancyResponse = await _vacancyResponseRepository.GetByIdAsync(request.VacancyResponseId);

        if (vacancyResponse.Candidate.UserId != request.UserId && !request.Roles.Contains("DeanMember"))
            throw new Forbidden("You do not have access to leave comment");

        var comment = new VacancyResponseCommentEntity
        {
            Content = request.Comment,
            UserId = request.UserId,
            ParentId = request.VacancyResponseId,
            VacancyResponse = vacancyResponse
        };

        await _vacancyResponseCommentRepository.AddAsync(comment);

        vacancyResponse.Comments.Add(comment);
        await _vacancyResponseRepository.UpdateAsync(vacancyResponse);

        var candidate = await _candidateRepository.GetCandidateByIdAsync(vacancyResponse.CandidateId);

        if (candidate != null && request.UserId != candidate.UserId)
        {
            var userToSend = await _userRepository.GetByIdAsync(candidate.UserId);
            var userFromSend = await _userRepository.GetByIdAsync(request.UserId);

            await _mediator.Send(new SendNewCommentMessageCommand(userToSend.Email, CommentType.vacancy_response,
                request.Comment, userFromSend.Surname + " " + userFromSend.Name, candidate.Selection.Id), cancellationToken);
        }

        return Unit.Value;
    }
}
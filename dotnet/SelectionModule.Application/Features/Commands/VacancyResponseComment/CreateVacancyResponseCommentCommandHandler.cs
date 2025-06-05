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
    private readonly IUserRepository _userRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;
    private readonly IVacancyResponseCommentRepository _vacancyResponseCommentRepository;

    public CreateVacancyResponseCommentCommandHandler(IVacancyResponseRepository vacancyResponseRepository,
        IVacancyResponseCommentRepository vacancyResponseCommentRepository, ISender mediator, IUserRepository userRepository)
    {
        _vacancyResponseRepository = vacancyResponseRepository;
        _vacancyResponseCommentRepository = vacancyResponseCommentRepository;
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(CreateVacancyResponseCommentCommand request, CancellationToken cancellationToken)
    {
        if (!await _vacancyResponseRepository.CheckIfExistsAsync(request.VacancyResponseId))
            throw new NotFound("Selection not found");

        var vacancyResponse = await _vacancyResponseRepository.GetByIdAsync(request.VacancyResponseId);

        if (vacancyResponse.Candidate.UserId != request.UserId && !request.Roles.Contains("DeanMember"))
            throw new Forbidden("You do not have access to leave comment");

        await _vacancyResponseCommentRepository.AddAsync(new VacancyResponseCommentEntity
        {
            Content = request.Comment,
            UserId = request.UserId,
            ParentId = request.VacancyResponseId,
            VacancyResponse = vacancyResponse
        });

        if (request.Roles.Contains("DeanMember"))
        {
            var userToSend = await _userRepository.GetByIdAsync(vacancyResponse.Candidate.UserId);
            
            var user = await _userRepository.GetByIdAsync(userToSend.Id);
            
            await _mediator.Send(new SendNewCommentMessageCommand(userToSend.Email, CommentType.selection, request.Comment,
                user.Surname + user.Name, vacancyResponse.Candidate.Selection.Id), cancellationToken);
        }
        
        return Unit.Value;
    }
}
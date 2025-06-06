using MediatR;
using SelectionModule.Contracts.Commands.VacancyResponseComment;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.VacancyResponseComment;

public class CreateVacancyResponseCommentCommandHandler : IRequestHandler<CreateVacancyResponseCommentCommand, Unit>
{
    private readonly IVacancyResponseRepository _vacancyResponseRepository;
    private readonly IVacancyResponseCommentRepository _vacancyResponseCommentRepository;

    public CreateVacancyResponseCommentCommandHandler(IVacancyResponseRepository vacancyResponseRepository,
        IVacancyResponseCommentRepository vacancyResponseCommentRepository)
    {
        _vacancyResponseRepository = vacancyResponseRepository;
        _vacancyResponseCommentRepository = vacancyResponseCommentRepository;
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
        
        return Unit.Value;
    }
}
using MediatR;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using Shared.Contracts.Dtos;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Queries;

public class GetVacancyResponseCommentsQueryHandler : IRequestHandler<GetVacancyResponseCommentsQuery, List<CommentDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;

    public GetVacancyResponseCommentsQueryHandler(IUserRepository userRepository,
        IVacancyResponseRepository vacancyResponseRepository)
    {
        _userRepository = userRepository;
        _vacancyResponseRepository = vacancyResponseRepository;
    }


    public async Task<List<CommentDto>> Handle(GetVacancyResponseCommentsQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _vacancyResponseRepository.CheckIfExistsAsync(request.VacancyResponseId))
            throw new NotFound("Selection not found");

        var vacancyResponse = await _vacancyResponseRepository.GetByIdAsync(request.VacancyResponseId);

        if (vacancyResponse.Candidate.UserId != request.UserId && !request.Roles.Contains("DeanMember"))
            throw new Forbidden("You do not have access to leave comment");

        var comments = vacancyResponse.Comments;

        var result = new List<CommentDto>();

        foreach (var comment in comments)
        {
            var user = await _userRepository.GetByIdAsync(comment.UserId);

            result.Add(new CommentDto
            {
                Id = comment.Id,
                IsDeleted = comment.IsDeleted,
                Content = comment.Content,
                Author = new CommentUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname
                }
            });
        }

        return result;
    }
}
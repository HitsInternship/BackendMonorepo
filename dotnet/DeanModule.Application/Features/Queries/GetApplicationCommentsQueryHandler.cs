using DeanModule.Contracts.Queries;
using DeanModule.Contracts.Repositories;
using MediatR;
using Shared.Contracts.Dtos;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Queries;

public class GetApplicationCommentsQueryHandler : IRequestHandler<GetApplicationCommentsQuery, List<CommentDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationRepository _applicationRepository;

    public GetApplicationCommentsQueryHandler(IApplicationRepository applicationRepository,
        IUserRepository userRepository)
    {
        _applicationRepository = applicationRepository;
        _userRepository = userRepository;
    }

    public async Task<List<CommentDto>> Handle(GetApplicationCommentsQuery request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new NotFound("Application not found");

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        var result = new List<CommentDto>();

        foreach (var comment in application.Comments)
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
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using MediatR;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Queries;

public class GetDeanMemberQueryHandler : IRequestHandler<GetDeanMemberQuery, DeanMemberDto>
{
    private readonly IUserRepository _userRepository;

    public GetDeanMemberQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<DeanMemberDto> Handle(GetDeanMemberQuery request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfExistsAsync(request.UserId))
            throw new NotFound("User not found");

        var user = await _userRepository.GetByIdAsync(request.UserId);

        return new DeanMemberDto
        {
            Id = user.Id,
            IsDeleted = user.IsDeleted,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
        };
    }
}
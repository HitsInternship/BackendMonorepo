using DeanModule.Contracts.Commands.DeanMember;
using DeanModule.Contracts.Repositories;
using MediatR;
using UserModule.Contracts.Commands;
using UserModule.Contracts.DTOs.Requests;
using UserModule.Domain.Enums;

namespace DeanModule.Application.Features.Commands.DeanMember;

public class CreateDeanMemberCommandHandler : IRequestHandler<CreateDeanMemberCommand, Unit>
{
    private readonly ISender _mediator;

    public CreateDeanMemberCommandHandler(ISender mediator)
    {
        _mediator = mediator;
    }


    public async Task<Unit> Handle(CreateDeanMemberCommand request, CancellationToken cancellationToken)
    {
        var userRequest = new UserRequest
        {
            name = request.DeanMemberRequestDto.Name,
            surname = request.DeanMemberRequestDto.Surname,
            email = request.DeanMemberRequestDto.Email,
        };

        if (request.UserId.HasValue)
        {
            await _mediator.Send(new AddUserRoleCommand(request.UserId.Value, RoleName.DeanMember), cancellationToken);
        }
        else
        {
            var user = await _mediator.Send(new CreateUserCommand(userRequest, request.Password), cancellationToken);
            await _mediator.Send(new AddUserRoleCommand(user.Id, RoleName.DeanMember), cancellationToken);
        }

        return Unit.Value;
    }
}
using DeanModule.Contracts.Dtos.Requests;
using MediatR;

namespace DeanModule.Contracts.Commands.DeanMember;

public record CreateDeanMemberCommand(DeanMemberRequestDto DeanMemberRequestDto) : IRequest<Unit>;
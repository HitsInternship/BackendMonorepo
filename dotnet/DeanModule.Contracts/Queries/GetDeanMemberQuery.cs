using DeanModule.Contracts.Dtos.Responses;
using MediatR;

namespace DeanModule.Contracts.Queries;

public record GetDeanMemberQuery(Guid UserId) : IRequest<DeanMemberDto>;
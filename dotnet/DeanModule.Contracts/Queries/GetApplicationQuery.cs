using DeanModule.Contracts.Dtos.Responses;
using MediatR;

namespace DeanModule.Contracts.Queries;

public record GetApplicationQuery(Guid ApplicationId, Guid UserId, List<string> Roles) : IRequest<ApplicationResponseDto>;
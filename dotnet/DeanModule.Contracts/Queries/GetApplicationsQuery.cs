using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Domain.Enums;
using MediatR;

namespace DeanModule.Contracts.Queries;

public record GetApplicationsQuery(
    ApplicationStatus? ApplicationStatus,
    string? Name,
    bool IsArchives,
    int Page,
    Guid UserId,
    List<string> Roles)
    : IRequest<ApplicationsDto>;
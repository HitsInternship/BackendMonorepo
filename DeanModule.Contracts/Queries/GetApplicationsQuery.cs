using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Domain.Enums;
using MediatR;

namespace DeanModule.Contracts.Queries;

public record GetApplicationsQuery(ApplicationStatus? ApplicationStatus, Guid? StudentId, bool IsArchives, int Page)
    : IRequest<ApplicationsDto>;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeanModule.Contracts.Queries;

public record DownloadApplicationFileCommand(Guid ApplicationId, Guid UserId, List<string> Roles)
    : IRequest<FileContentResult>;
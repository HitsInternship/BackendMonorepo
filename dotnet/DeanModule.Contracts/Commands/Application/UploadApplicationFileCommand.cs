using DeanModule.Contracts.Dtos.Requests;
using MediatR;

namespace DeanModule.Contracts.Commands.Application;

public record UploadApplicationFileCommand(Guid ApplicationId, UploadFileRequest File, Guid UserId) : IRequest<Guid>;
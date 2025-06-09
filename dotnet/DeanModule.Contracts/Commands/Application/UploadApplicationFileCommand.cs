using DeanModule.Contracts.Dtos.Requests;
using MediatR;
using Shared.Contracts.Dtos;

namespace DeanModule.Contracts.Commands.Application;

public record UploadApplicationFileCommand(Guid ApplicationId, UploadFileRequest File, Guid UserId) : IRequest<Guid>;
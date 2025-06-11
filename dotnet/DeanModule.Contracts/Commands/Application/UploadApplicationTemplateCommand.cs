using DeanModule.Contracts.Dtos.Requests;
using MediatR;

namespace DeanModule.Contracts.Commands.Application;

public record UploadApplicationTemplateCommand(UploadFileRequest File) : IRequest<Unit>;
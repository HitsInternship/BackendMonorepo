using MediatR;
using Shared.Contracts.Dtos;

namespace DeanModule.Contracts.Commands.Application;

public record UploadApplicationTemplateCommand(UploadFileRequestDto File) : IRequest<Unit>;
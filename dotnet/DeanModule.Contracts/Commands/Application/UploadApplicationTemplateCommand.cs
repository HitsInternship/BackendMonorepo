using DeanModule.Contracts.Dtos.Requests;
using MediatR;
using Shared.Contracts.Dtos;

namespace DeanModule.Contracts.Commands.Application;

public record UploadApplicationTemplateCommand(UploadFileRequest File) : IRequest<Unit>;
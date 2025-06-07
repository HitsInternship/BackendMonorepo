using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeanModule.Contracts.Queries;

public record GetApplicationTemplateCommand() : IRequest<FileContentResult>;
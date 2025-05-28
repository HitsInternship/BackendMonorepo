using DeanModule.Contracts.Dtos.Requests;
using MediatR;

namespace DeanModule.Contracts.Commands.Application;

public record CreateApplicationCommand(ApplicationRequestDto ApplicationRequestDto) : IRequest<Unit>;
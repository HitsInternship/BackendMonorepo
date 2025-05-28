using DeanModule.Contracts.Dtos.Requests;
using MediatR;

namespace DeanModule.Contracts.Commands.Semester;

public record UpdateSemesterCommand(Guid SemesterId, SemesterRequestDto SemesterRequestDto) : IRequest<Unit>;
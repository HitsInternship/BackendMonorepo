using DeanModule.Contracts.Dtos.Requests;
using MediatR;

namespace DeanModule.Contracts.Commands.Semester;

public record CreateSemesterCommand(SemesterRequestDto SemesterRequestDto) : IRequest<Unit>;
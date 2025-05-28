using MediatR;

namespace DeanModule.Contracts.Commands.Semester;

public record DeleteSemesterCommand(Guid SemesterId, bool IsArchive) : IRequest<Unit>;
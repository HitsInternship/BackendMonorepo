using MediatR;

namespace DeanModule.Contracts.Commands.StreamSemester;

public record DeleteStreamSemesterCommand(Guid StreamSemesterId, bool IsArchive) : IRequest<Unit>;
using DeanModule.Contracts.Dtos.Requests;
using MediatR;

namespace DeanModule.Contracts.Commands.StreamSemester;

public record UpdateStreamSemester(Guid StreamSemesterId, StreamSemesterRequestDto StreamSemesterRequestDto) : IRequest<Unit>;
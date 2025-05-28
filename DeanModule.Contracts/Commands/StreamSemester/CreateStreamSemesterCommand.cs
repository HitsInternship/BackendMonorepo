using DeanModule.Contracts.Dtos.Requests;
using MediatR;

namespace DeanModule.Contracts.Commands.StreamSemester;

public record CreateStreamSemesterCommand(StreamSemesterRequestDto SemesterRequestDto) : IRequest<Unit>;
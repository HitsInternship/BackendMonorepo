using DeanModule.Contracts.Dtos.Responses;
using MediatR;

namespace DeanModule.Contracts.Queries;

public record GetStreamSemestersByStreamQuery(Guid StreamId) : IRequest<List<StreamSemesterResponseDto>>;
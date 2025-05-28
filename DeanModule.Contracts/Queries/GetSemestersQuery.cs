using DeanModule.Contracts.Dtos.Responses;
using MediatR;

namespace DeanModule.Contracts.Queries;

public record GetSemestersQuery(bool IsArchive) : IRequest<List<SemesterResponseDto>>;
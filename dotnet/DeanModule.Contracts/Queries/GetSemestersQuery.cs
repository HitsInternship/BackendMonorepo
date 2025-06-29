using DeanModule.Contracts.Dtos.Responses;
using MediatR;
using Shared.Contracts.Dtos;

namespace DeanModule.Contracts.Queries;

public record GetSemestersQuery(bool IsArchive) : IRequest<List<SemesterResponseDto>>;
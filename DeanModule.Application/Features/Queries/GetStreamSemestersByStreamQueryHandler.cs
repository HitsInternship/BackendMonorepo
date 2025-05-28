using AutoMapper;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using DeanModule.Contracts.Repositories;
using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Queries;

public class
    GetStreamSemestersByStreamQueryHandler : IRequestHandler<GetStreamSemestersByStreamQuery,
    List<StreamSemesterResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IStreamSemesterRepository _streamSemesterRepository;
    private readonly IStreamRepository _streamRepository;

    public GetStreamSemestersByStreamQueryHandler(IMapper mapper, IStreamSemesterRepository streamSemesterRepository,
        IStreamRepository streamRepository)
    {
        _mapper = mapper;
        _streamSemesterRepository = streamSemesterRepository;
        _streamRepository = streamRepository;
    }

    public async Task<List<StreamSemesterResponseDto>> Handle(GetStreamSemestersByStreamQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _streamRepository.CheckIfExistsAsync(request.StreamId))
            throw new NotFound("Stream does not exist");

        var semesters = (await _streamSemesterRepository.GetByStreamIdAsync(request.StreamId)).ToList();

        var stream = await _streamRepository.GetStreamByIdAsync(request.StreamId);

        var streamDto = _mapper.Map<StreamDto>(stream);
        
        var result = new List<StreamSemesterResponseDto>();

        foreach (var dto in semesters.Select(semester => _mapper.Map<StreamSemesterResponseDto>(semester)))
        {
            dto.Stream = streamDto;

            result.Add(dto);
        }

        return result;
    }
}
using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Commands.Semester;
using DeanModule.Contracts.Commands.StreamSemester;
using DeanModule.Contracts.Dtos.Requests;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Commands.StreamSemester;

public class UpdateStreamSemesterCommandHandler : IRequestHandler<UpdateStreamSemester, Unit>
{
    private readonly ISemesterRepository _semesterRepository;
    private readonly IStreamRepository _streamRepository;
    private readonly IStreamSemesterRepository _streamSemesterRepository;

    public UpdateStreamSemesterCommandHandler(ISemesterRepository semesterRepository,
        IStreamSemesterRepository streamSemesterRepository, IStreamRepository streamRepository)
    {
        _semesterRepository = semesterRepository;
        _streamSemesterRepository = streamSemesterRepository;
        _streamRepository = streamRepository;
    }


    public async Task<Unit> Handle(UpdateStreamSemester request, CancellationToken cancellationToken)
    {
        if (!await _streamSemesterRepository.CheckIfExistsAsync(request.StreamSemesterId))
            throw new StreamSemesterNotFound(request.StreamSemesterId);

        if (!await _streamRepository.CheckIfExistsAsync(request.StreamSemesterRequestDto.StreamId))
            throw new StreamSemesterNotFound(request.StreamSemesterRequestDto.StreamId);

        if (!await _semesterRepository.CheckIfExistsAsync(request.StreamSemesterRequestDto.SemesterId))
            throw new SemesterNotFound(request.StreamSemesterRequestDto.StreamId);

        if (await _streamSemesterRepository.CheckIfStreamSemesterExistsAsync(
                request.StreamSemesterRequestDto.SemesterId, request.StreamSemesterRequestDto.StreamId))
            throw new BadRequest("");

        if (request.StreamSemesterRequestDto.Number <= 0) throw new BadRequest("Number must be greater than 0");

        var streamSemester = await _streamSemesterRepository.GetByIdAsync(request.StreamSemesterId);

        await UpdateEntityAsync(streamSemester, request.StreamSemesterRequestDto);

        await _streamSemesterRepository.UpdateAsync(streamSemester);

        return Unit.Value;
    }

    private async Task UpdateEntityAsync(StreamSemesterEntity entity, StreamSemesterRequestDto dto)
    {
        entity.StreamId = dto.StreamId;
        entity.SemesterId = dto.SemesterId;
        entity.Number = dto.Number;
        entity.SemesterEntity = await _semesterRepository.GetByIdAsync(dto.SemesterId);
    }
}
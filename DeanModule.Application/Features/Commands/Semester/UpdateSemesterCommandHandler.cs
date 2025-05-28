using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Commands.Semester;
using DeanModule.Contracts.Dtos.Requests;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;

namespace DeanModule.Application.Features.Commands.Semester;

public class UpdateSemesterCommandHandler : IRequestHandler<UpdateSemesterCommand, Unit>
{
    private readonly ISemesterRepository _semesterRepository;

    public UpdateSemesterCommandHandler(ISemesterRepository semesterRepository)
    {
        _semesterRepository = semesterRepository;
    }

    public async Task<Unit> Handle(UpdateSemesterCommand request, CancellationToken cancellationToken)
    {
        if (!await _semesterRepository.CheckIfExistsAsync(request.SemesterId))
            throw new SemesterNotFound(request.SemesterId);

        var semester = await _semesterRepository.GetByIdAsync(request.SemesterId);

        UpdateSemester(semester, request.SemesterRequestDto);

        await _semesterRepository.UpdateAsync(semester);
        
        return Unit.Value;
    }

    private static void UpdateSemester(SemesterEntity semester, SemesterRequestDto dto)
    {
        semester.Description = dto.Description;
        semester.StartDate = dto.StartDate;
        semester.EndDate = dto.EndDate;
    }
}
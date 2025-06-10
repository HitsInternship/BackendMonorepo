using CompanyModule.Contracts.Repositories;
using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Dtos.Requests;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using DeanModule.Domain.Enums;
using MediatR;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Commands.Application;

public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, Unit>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IApplicationRepository _applicationRepository;

    public UpdateApplicationCommandHandler(IApplicationRepository applicationRepository,
        IStudentRepository studentRepository, IPositionRepository positionRepository,
        ICompanyRepository companyRepository)
    {
        _applicationRepository = applicationRepository;
        _studentRepository = studentRepository;
        _positionRepository = positionRepository;
        _companyRepository = companyRepository;
    }

    public async Task<Unit> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new ApplicationNotFound(request.ApplicationId);

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        if ((application.Status is ApplicationStatus.Rejected or ApplicationStatus.Accepted) || application.IsDeleted)
            throw new BadRequest("You cannot update a rejected or accepted or archived application");

        var student = await _studentRepository.GetByIdAsync(application.StudentId);

        if (student.UserId != request.UserId && !request.Roles.Contains("DeanMember"))
            throw new Forbidden("You cannot update this application.");

        if (!await _companyRepository.CheckIfExistsAsync(request.ApplicationRequestDto.CompanyId))
            throw new BadRequest("Company not found");

        if (!await _positionRepository.CheckIfExistsAsync(request.ApplicationRequestDto.PositionId))
            throw new BadRequest("Position not found");

        UpdateApplication(application, request.ApplicationRequestDto);

        await _applicationRepository.UpdateAsync(application);

        return Unit.Value;
    }

    private void UpdateApplication(ApplicationEntity application, ApplicationRequestDto dto)
    {
        application.Date = dto.Date;
        application.Description = dto.Description;
        application.PositionId = dto.PositionId;
        application.CompanyId = dto.CompanyId;
    }
}
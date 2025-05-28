using AutoMapper;
using CompanyModule.Contracts.Repositories;
using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Commands.Application;

public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPositionRepository _positionRepository;

    public CreateApplicationCommandHandler(IApplicationRepository applicationRepository, IMapper mapper,
        IStudentRepository studentRepository)
    {
        _applicationRepository = applicationRepository;
        _mapper = mapper;
        _studentRepository = studentRepository;
    }

    public async Task<Unit> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        if (!await _studentRepository.CheckIfExistsAsync(request.ApplicationRequestDto.StudentId))
            throw new NotFound("Student not found");

        if (!await _companyRepository.CheckIfExistsAsync(request.ApplicationRequestDto.CompanyId))
            throw new NotFound("Company not found");

        if (!await _positionRepository.CheckIfExistsAsync(request.ApplicationRequestDto.PositionId))
            throw new NotFound("Position not found");

        var application = _mapper.Map<ApplicationEntity>(request.ApplicationRequestDto);

        //todo: add document

        await _applicationRepository.AddAsync(application);

        return Unit.Value;
    }
}
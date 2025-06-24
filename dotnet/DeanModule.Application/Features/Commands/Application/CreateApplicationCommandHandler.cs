using AutoMapper;
using CompanyModule.Contracts.Repositories;
using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using PracticeModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Commands.Application;

public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPositionRepository _positionRepository;

    public CreateApplicationCommandHandler(IApplicationRepository applicationRepository, IMapper mapper,
        IStudentRepository studentRepository, IPositionRepository positionRepository,
        ICompanyRepository companyRepository, ISender mediator)
    {
        _applicationRepository = applicationRepository;
        _mapper = mapper;
        _studentRepository = studentRepository;
        _positionRepository = positionRepository;
        _companyRepository = companyRepository;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByUserIdAsync(request.UserId) ??
                      throw new BadRequest("You are not a student");

        if (!await _companyRepository.CheckIfExistsAsync(request.ApplicationRequestDto.CompanyId))
            throw new BadRequest("Company not found");

        if (!await _positionRepository.CheckIfExistsAsync(request.ApplicationRequestDto.PositionId))
            throw new BadRequest("Position not found");

        if (student.UserId != request.UserId) throw new Forbidden("You cannot create a new application");

        var oldPractice = await _mediator.Send(new GetStudentPracticeQuery(student.Id), cancellationToken);

        if (oldPractice == null) throw new BadRequest("Practice not found");

        var application = _mapper.Map<ApplicationEntity>(request.ApplicationRequestDto);
        application.OldPractice = oldPractice.Id;

        application.StudentId = student.Id;

        await _applicationRepository.AddAsync(application);

        return application.Id;
    }
}
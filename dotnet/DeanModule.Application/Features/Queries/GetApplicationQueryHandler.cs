using AutoMapper;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Repositories;
using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using DeanModule.Contracts.Repositories;
using MediatR;
using PracticeModule.Contracts.Queries;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Queries;

public class GetApplicationQueryHandler : IRequestHandler<GetApplicationQuery, ApplicationResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IApplicationRepository _applicationRepository;

    public GetApplicationQueryHandler(IMapper mapper, IApplicationRepository applicationRepository,
        ICompanyRepository companyRepository, IPositionRepository positionRepository,
        IStudentRepository studentRepository, IUserRepository userRepository, IMediator mediator)
    {
        _mapper = mapper;
        _applicationRepository = applicationRepository;
        _companyRepository = companyRepository;
        _positionRepository = positionRepository;
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<ApplicationResponseDto> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new ApplicationNotFound(request.ApplicationId);

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);
        var student = await _studentRepository.GetStudentByIdAsync(application.StudentId);

        if (!request.Roles.Contains("DeanMember"))
        {
            if (student.UserId != request.UserId)
                throw new Forbidden("You are not allowed to access this page");

            if (application.IsDeleted)
                throw new BadRequest("The application is deleted");
        }

        var dto = _mapper.Map<ApplicationResponseDto>(application);

        var user = await _userRepository.GetByIdAsync(student.UserId);
        var oldPractice = await _mediator.Send(new GetStudentPracticeQuery(student.Id), cancellationToken);

        dto.NewPosition =
            _mapper.Map<PositionDto>(await _positionRepository.GetByIdAsync(application.PositionId));
        dto.NewCompany =
            _mapper.Map<CompanyResponse>(await _companyRepository.GetByIdAsync(application.CompanyId));
        dto.OldCompany =
            _mapper.Map<CompanyResponse>(await _companyRepository.GetByIdAsync(oldPractice.CompanyId));
        dto.OldPosition =
            _mapper.Map<PositionDto>(await _positionRepository.GetByIdAsync(oldPractice.PositionId));
        dto.Student = new StudentDto(student)
        {
            Name = user.Name,
            Email = user.Email,
            Surname = user.Surname
        };

        return dto;
    }
}
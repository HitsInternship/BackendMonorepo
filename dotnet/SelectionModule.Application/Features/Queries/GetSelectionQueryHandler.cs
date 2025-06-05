using AutoMapper;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Queries;

public class GetSelectionQueryHandler : IRequestHandler<GetSelectionQuery, SelectionDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IStudentRepository _studentRepository;

    public GetSelectionQueryHandler(IMapper mapper, ISelectionRepository selectionRepository,
        IVacancyResponseRepository vacancyResponseRepository, ICandidateRepository candidateRepository,
        IStudentRepository studentRepository, IUserRepository userRepository, ICompanyRepository companyRepository)
    {
        _mapper = mapper;
        _vacancyResponseRepository = vacancyResponseRepository;
        _candidateRepository = candidateRepository;
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _companyRepository = companyRepository;
    }

    public async Task<SelectionDto> Handle(GetSelectionQuery request, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetCandidateByStudentIdAsync(request.StudentId) ??
                        throw new BadRequest("User does not have selection.");

        var selection = candidate.Selection ?? throw new BadRequest("No selection .");

        var vacancyResponses = await _vacancyResponseRepository.GetByCandidateIdAsync(candidate.Id);

        var student = await _studentRepository.GetStudentByIdAsync(candidate.StudentId);

        var user = await _userRepository.GetByIdAsync(candidate.UserId);

        var candidateDto = new CandidateDto
        {
            Id = candidate.Id,
            IsDeleted = candidate.IsDeleted,
            Name = user.Name,
            Surname = user.Surname,
            Middlename = student.Middlename,
            Email = user.Email,
            Phone = student.Phone,
            GroupNumber = student.Group.GroupNumber
        };

        var vacanciesDtos = new List<SelectionVacancyResponseDto>();

        foreach (var vacancy in vacancyResponses)
        {
            vacanciesDtos.Add(new SelectionVacancyResponseDto
            {
                Id = vacancy.Id,
                IsDeleted = vacancy.IsDeleted,
                Company = _mapper.Map<ShortenCompanyDto>(
                    await _companyRepository.GetByIdAsync(vacancy.Vacancy.CompanyId)),
                Status = vacancy.Status,
                Position = vacancy.Vacancy.Position.Name
            });
        }

        return new SelectionDto
        {
            Id = selection.Id,
            IsDeleted = selection.IsDeleted,
            DeadLine = selection.DeadLine,
            SelectionStatus = selection.SelectionStatus,
            Candidate = candidateDto,
            Responses = vacanciesDtos
        };
    }
}
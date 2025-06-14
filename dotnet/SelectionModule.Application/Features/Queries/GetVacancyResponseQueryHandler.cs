using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Queries;

public class GetVacancyResponseQueryHandler : IRequestHandler<GetVacancyResponsesQuery, List<VacancyResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;

    public GetVacancyResponseQueryHandler(IVacancyRepository vacancyRepository, IStudentRepository studentRepository,
        ICandidateRepository candidateRepository, IVacancyResponseRepository vacancyResponseRepository,
        IUserRepository userRepository)
    {
        _vacancyRepository = vacancyRepository;
        _studentRepository = studentRepository;
        _candidateRepository = candidateRepository;
        _vacancyResponseRepository = vacancyResponseRepository;
        _userRepository = userRepository;
    }

    public async Task<List<VacancyResponseDto>> Handle(GetVacancyResponsesQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _vacancyRepository.CheckIfExistsAsync(request.VacancyId))
            throw new NotFound("Vacancy not found");

        var vacancyResponses = await _vacancyResponseRepository.FindAsync(x => x.VacancyId == request.VacancyId);

        var vacancyResponsesDto = new List<VacancyResponseDto>();

        foreach (var vacancyResponse in vacancyResponses)
        {
            var candidate = await _candidateRepository.GetByIdAsync(vacancyResponse.CandidateId);
            var student = await _studentRepository.GetStudentByIdAsync(candidate.StudentId);
            var user = await _userRepository.GetByIdAsync(student.UserId);

            vacancyResponsesDto.Add(new VacancyResponseDto
            {
                Id = vacancyResponse.Id,
                IsDeleted = vacancyResponse.IsDeleted,
                Candidate = new CandidateDto
                {
                    Id = candidate.Id,
                    IsDeleted = candidate.IsDeleted,
                    Name = user.Name,
                    Surname = user.Surname,
                    Middlename = student.Middlename,
                    Email = user.Email,
                    Phone = student.Phone,
                    GroupNumber = student.Group.GroupNumber
                },
                Status = vacancyResponse.Status,
            });
        }

        return vacancyResponsesDto;
    }
}
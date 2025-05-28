using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Queries;

public class GetVacancyResponseQueryHandler : IRequestHandler<GetVacancyResponsesQuery, List<VacancyResponseDto>>
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICandidateRepository _candidateRepository;

    public GetVacancyResponseQueryHandler(IVacancyRepository vacancyRepository, IStudentRepository studentRepository,
        ICandidateRepository candidateRepository)
    {
        _vacancyRepository = vacancyRepository;
        _studentRepository = studentRepository;
        _candidateRepository = candidateRepository;
    }

    public async Task<List<VacancyResponseDto>> Handle(GetVacancyResponsesQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _vacancyRepository.CheckIfExistsAsync(request.VacancyId))
            throw new NotFound("Vacancy not found");

        var vacancy = await _vacancyRepository.GetByIdAsync(request.VacancyId);
        var vacancyResponses = vacancy.Responses;

        var vacancyResponsesDto = new List<VacancyResponseDto>();

        foreach (var vacancyResponse in vacancyResponses)
        {
            var candidate = await _candidateRepository.GetByIdAsync(vacancyResponse.CandidateId);
            var student = await _studentRepository.GetByIdAsync(candidate.StudentId);

            vacancyResponsesDto.Add(new VacancyResponseDto
            {
                Id = vacancyResponse.Id,
                IsDeleted = vacancyResponse.IsDeleted,
                Candidate = new CandidateDto
                {
                    Id = candidate.Id,
                    IsDeleted = candidate.IsDeleted,
                    Name = student.User.Name,
                    Surname = student.User.Surname,
                    Middlename = student.Middlename,
                    Email = student.User.Email,
                    Phone = student.Phone,
                    GroupNumber = student.Group.GroupNumber
                },
                Status = vacancyResponse.Status,
            });
        }

        return vacancyResponsesDto;
    }
}
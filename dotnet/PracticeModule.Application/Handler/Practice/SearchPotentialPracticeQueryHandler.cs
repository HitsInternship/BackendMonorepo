using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using DeanModule.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;
using PracticeModule.Domain.Entity;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using SelectionModule.Domain.Enums;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using StudentModule.Domain.Enums;
using UserModule.Contracts.Queries;
using UserModule.Domain.Entities;

namespace PracticeModule.Application.Handler.PracticePart;

public class SearchPotentialPracticeQueryHandler : IRequestHandler<SearchPotentialPracticeQuery, List<Domain.Entity.Practice>>
{
    private readonly IPracticeRepository _practiceRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly IGlobalPracticeRepository _globalPracticeRepository;

    private readonly ISelectionRepository _selectionRepository;
    private readonly IVacancyRepository _vacancyRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IApplicationRepository _applicationRepository;

    private readonly IStudentRepository _studentRepository;
    private readonly ISender _sender;

    public SearchPotentialPracticeQueryHandler(IPracticeRepository practiceRepository,
        ISemesterRepository semesterRepository, IGlobalPracticeRepository globalPracticeRepository, ISelectionRepository selectionRepository,
        IVacancyRepository vacancyRepository, ICompanyRepository companyRepository,
        IPositionRepository positionRepository, IApplicationRepository applicationRepository,
        IStudentRepository studentRepository, ISender sender)
    {
        _practiceRepository = practiceRepository;
        _studentRepository = studentRepository;
        _globalPracticeRepository = globalPracticeRepository;
        _selectionRepository = selectionRepository;
        _vacancyRepository = vacancyRepository;
        _companyRepository = companyRepository;
        _positionRepository = positionRepository;
        _semesterRepository = semesterRepository;
        _applicationRepository = applicationRepository;
        _sender = sender;
    }

    public async Task<List<Domain.Entity.Practice>> Handle(SearchPotentialPracticeQuery query,
        CancellationToken cancellationToken)
    {
        SemesterEntity? lastSemester = await _semesterRepository.GetByIdAsync(query.searchRequest.lastSemesterId);
        if (lastSemester == null) { throw new NotFound("No last semester"); }

        List<Domain.Entity.Practice> potentialPractices = new List<Domain.Entity.Practice>();

        IQueryable<StudentEntity> studentDbQuery = (await _studentRepository.ListAllAsync()).Where(student => student.Group.StreamId == query.searchRequest.streamId);

        List<StudentEntity> students;
        List<Guid> studentIds;
        List<User> users;

        if (query.searchRequest.groupId != null)
        {
            studentDbQuery = studentDbQuery.Where(student => student.GroupId == query.searchRequest.groupId);
        }

        if (query.searchRequest.oldCompanyId != null)
        {
            var studentInCompanyIds = (await _practiceRepository.ListAllAsync())
                .Where(practice => practice.GlobalPractice.SemesterId == lastSemester.Id &&
                                   practice.Company.Id == query.searchRequest.oldCompanyId)
                .Select(practice => practice.StudentId);
            studentDbQuery = studentDbQuery.Where(student => studentInCompanyIds.Contains(student.Id));
        }

        if (query.searchRequest.oldPositionId != null)
        {
            var studentInPositionIds = (await _practiceRepository.ListAllAsync())
                .Where(practice => practice.GlobalPractice.SemesterId == lastSemester.Id &&
                                   practice.Position.Id == query.searchRequest.oldPositionId)
                .Select(practice => practice.StudentId);
            studentDbQuery = studentDbQuery.Where(student => studentInPositionIds.Contains(student.Id));
        }

        students = studentDbQuery.ToList();
        studentIds = students.Select(student => student.Id).ToList();
        users = await _sender.Send(new GetListUserQuery(students.Select(student => student.UserId).ToList()));

        List<Domain.Entity.Practice> practices = (await _practiceRepository.ListAllAsync()).Where(practice =>
            practice.GlobalPractice.SemesterId == lastSemester.Id &&
            studentIds.Contains(practice.StudentId)).ToList();

        List<ApplicationEntity> applications = (await _applicationRepository.ListAllAsync()).Where(application =>
            studentIds.Contains(application.StudentId) &&
            application.Date > lastSemester.StartDate &&
            application.Date < lastSemester.EndDate &&
            application.Status == ApplicationStatus.Accepted).ToList();

        List<Company> companies = (await _companyRepository.ListAllAsync()).Where(company =>
            applications.Select(application => application.CompanyId).Contains(company.Id) ||
            practices.Select(practice => practice.CompanyId).Contains(company.Id)).ToList();
        List<PositionEntity> positions = (await _positionRepository.ListAllAsync()).Where(position =>
            applications.Select(application => application.PositionId).Contains(position.Id) ||
            practices.Select(practice => practice.PositionId).Contains(position.Id)).ToList();

        foreach (var practice in practices)
        {
            StudentEntity student = students.First(student => student.Id == practice.StudentId);
            student.User = users.First(user => user.Id == student.UserId);

            Company company = companies.First(company => company.Id == practice.CompanyId);
            PositionEntity position = positions.First(position => position.Id == practice.PositionId);

            ApplicationEntity? approvedApplication = applications.FirstOrDefault(application => application.StudentId == practice.StudentId);
            Company newCompany = companies.FirstOrDefault(company => company.Id == approvedApplication?.CompanyId);
            PositionEntity newPosition = positions.FirstOrDefault(position => position.Id == approvedApplication?.PositionId);

            potentialPractices.Add(new Domain.Entity.Practice()
            {
                Student = student, Company = company, Position = position, NewCompany = newCompany,
                NewPosition = newPosition
            });
        }

        List<SelectionEntity> selections = (await _selectionRepository.ListAllAsync()).Where(selection =>
                selection.GlobalSelection.SemesterId == lastSemester.Id &&
                studentIds.Contains(selection.Candidate.StudentId) &&
                selection.SelectionStatus == SelectionStatus.OffersAccepted).Include(selection => selection.Candidate)
            .ToList();

        List<VacancyEntity> vacancies = (await _vacancyRepository.ListAllAsync())
            .Where(vacancy => selections.Select(selection => selection.Offer).Contains(vacancy.Id))
            .Include(vacancy => vacancy.Position).ToList();
        companies = (await _companyRepository.ListAllAsync())
            .Where(company => vacancies.Select(vacancy => vacancy.CompanyId).Contains(company.Id)).ToList();

        foreach (var selection in selections)
        {
            StudentEntity student = students.First(student => student.Id == selection.Candidate.StudentId);
            student.User = users.First(user => user.Id == student.UserId);

            VacancyEntity newVacancy = vacancies.First(vacancy => vacancy.Id == selection.Offer);
            Company newCompany = companies.First(company => company.Id == newVacancy.CompanyId);

            PositionEntity newPosition = newVacancy.Position;

            potentialPractices.Add(new Domain.Entity.Practice()
                { Student = student, NewCompany = newCompany, NewPosition = newPosition });
        }

        return potentialPractices;
    }
}
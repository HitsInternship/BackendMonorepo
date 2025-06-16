using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using UserModule.Contracts.Queries;
using UserModule.Domain.Entities;

namespace PracticeModule.Application.Handler.Practice;

public class SearchPracticeQueryHandler : IRequestHandler<SearchPracticeQuery, List<Domain.Entity.Practice>>
{
    private readonly IPracticeRepository _practiceRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ISender _sender;
    public SearchPracticeQueryHandler(IPracticeRepository practiceRepository, ISemesterRepository semesterRepository, IStudentRepository studentRepository, ISender sender)
    {
        _practiceRepository = practiceRepository;
        _semesterRepository = semesterRepository;
        _studentRepository = studentRepository;
        _sender = sender;
    }

    public async Task<List<Domain.Entity.Practice>> Handle(SearchPracticeQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entity.Practice> dbQuery = (await _practiceRepository.ListAllAsync()).Where(practice => practice.GlobalPractice.Id == query.searchRequest.globalPracticeId);

        if (query.searchRequest.groupId != null)
        {
            List<Guid> studentInGroupIds = (await _studentRepository.ListAllAsync()).Where(student => student.Group.Id == query.searchRequest.groupId).Select(student => student.Id).ToList();

            dbQuery = dbQuery.Where(practice => studentInGroupIds.Contains(practice.StudentId));
        }

        if (query.searchRequest.companyId != null)
        {
            dbQuery = dbQuery.Where(practice => practice.CompanyId == query.searchRequest.companyId);
        }

        if (query.searchRequest.hasMark != null)
        {
            dbQuery = dbQuery.Where(practice => (practice.Mark != null) == query.searchRequest.hasMark);
        }

        List<Domain.Entity.Practice> practices = dbQuery.Include(practice => practice.PracticeDiary).Include(practice => practice.StudentPracticeCharacteristics).ToList();
        List<StudentEntity> students = (await _studentRepository.ListAllAsync()).Where(student => practices.Select(practice => practice.StudentId).Contains(student.Id)).ToList();
        List<User> users = await _sender.Send(new GetListUserQuery(students.Select(student => student.UserId).ToList()));

        foreach (Domain.Entity.Practice practice in practices)
        {
            StudentEntity studentTmp = students.First(student => student.Id == practice.StudentId);
            studentTmp.User = users.First(user => user.Id == studentTmp.UserId);

            practice.Student = studentTmp;
        }

        return practices;
    }
}
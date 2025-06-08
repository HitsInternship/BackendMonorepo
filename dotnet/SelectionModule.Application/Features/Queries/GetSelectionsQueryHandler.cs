using CompanyModule.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Queries;

public class GetSelectionsQueryHandler : IRequestHandler<GetSelectionsQuery, List<ListedSelectionDto>>
{
    private readonly ISelectionRepository _selectionRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IVacancyRepository _vacancyRepository;

    public GetSelectionsQueryHandler(ISelectionRepository selectionRepository, ICandidateRepository candidateRepository,
        IStudentRepository studentRepository, IUserRepository userRepository, IPositionRepository positionRepository,
        ICompanyRepository companyRepository, IVacancyRepository vacancyRepository)
    {
        _selectionRepository = selectionRepository;
        _candidateRepository = candidateRepository;
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _positionRepository = positionRepository;
        _companyRepository = companyRepository;
        _vacancyRepository = vacancyRepository;
    }

    public async Task<List<ListedSelectionDto>> Handle(GetSelectionsQuery request, CancellationToken cancellationToken)
    {
        List<Guid> userIds;
        IQueryable<StudentEntity> students = await _studentRepository.ListAllAsync();
        var selectionsEntity = await _selectionRepository.ListAllAsync();

        if (request.GroupNumber.HasValue)
        {
            students = students.Where(x => x.Group.GroupNumber == request.GroupNumber.Value);
            userIds = students.Select(s => s.UserId).ToList();
            selectionsEntity = selectionsEntity.Where(x => userIds.Contains(x.Id));
        }

        if (request.Status.HasValue)
        {
            selectionsEntity = selectionsEntity.Where(x => x.SelectionStatus == request.Status.Value);
        }

        if (request.IsArchive.HasValue)
        {
            selectionsEntity = selectionsEntity.Where(x => x.IsDeleted == false);
        }

        var selections = new List<ListedSelectionDto>();

        var selectionsEntityList = await selectionsEntity.ToListAsync(cancellationToken);

        foreach (var selectionEntity in selectionsEntityList)
        {
            var candidate = await _candidateRepository.GetByIdAsync(selectionEntity.CandidateId);
            var student = await _studentRepository.GetStudentByIdAsync(candidate.StudentId);
            var user = await _userRepository.GetByIdAsync(student.UserId);

            var selection = new ListedSelectionDto
            {
                Id = selectionEntity.Id,
                IsDeleted = selectionEntity.IsDeleted,
                SelectionStatus = selectionEntity.SelectionStatus,
                Candidate = new CandidateDto
                {
                    Id = candidate.Id,
                    IsDeleted = candidate.IsDeleted,
                    Name = user.Name,
                    Surname = user.Surname,
                    Middlename = student.Middlename,
                    Email = user.Email,
                    Phone = student.Phone,
                    GroupNumber = student.Group.GroupNumber,
                },
            };

            if (selectionEntity.Offer.HasValue)
            {
                var vacancy = await _vacancyRepository.GetByIdAsync(selectionEntity.Offer.Value);
                var position = await _positionRepository.GetByIdAsync(vacancy.PositionId);
                var company = await _companyRepository.GetByIdAsync(vacancy.CompanyId);


                selection.Offer = new OfferDto
                {
                    Position = position.Name,
                    CompanyName = company.Name,
                };
            }

            selections.Add(selection);
        }

        return selections;
    }
}
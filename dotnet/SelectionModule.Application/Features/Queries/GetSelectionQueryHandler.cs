using AutoMapper;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Queries;

public class GetSelectionQueryHandler : IRequestHandler<GetSelectionQuery, SelectionDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly ISelectionRepository _selectionRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly ICuratorRepository _curatorRepository;

    public GetSelectionQueryHandler(IVacancyRepository vacancyRepository, IUserRepository userRepository,
        ISelectionRepository selectionRepository, IMapper mapper, IVacancyResponseRepository vacancyResponseRepository,
        ICompanyRepository companyRepository, IStudentRepository studentRepository,
        IPositionRepository positionRepository, ICandidateRepository candidateRepository,
        ICuratorRepository curatorRepository)
    {
        _vacancyRepository = vacancyRepository;
        _userRepository = userRepository;
        _selectionRepository = selectionRepository;
        _mapper = mapper;
        _vacancyResponseRepository = vacancyResponseRepository;
        _companyRepository = companyRepository;
        _studentRepository = studentRepository;
        _positionRepository = positionRepository;
        _candidateRepository = candidateRepository;
        _curatorRepository = curatorRepository;
    }


    public async Task<SelectionDto> Handle(GetSelectionQuery request, CancellationToken cancellationToken)
    {
        CandidateEntity candidate;
        SelectionEntity selection;

        if (request.StudentId.HasValue)
        {
            candidate = await _candidateRepository.GetCandidateByStudentIdAsync(request.StudentId.Value) ?? throw new
                BadRequest("You are not a candidate");

            if (candidate.Selection != null)
            {
                selection = candidate.Selection;
            }
            else
            {
                var selectionEntity = await _selectionRepository.GetByCandidateIdAsync(candidate.Id);

                selection = selectionEntity ?? throw new NotFound("Selection not found");
            }
        }
        else
        {
            selection = await _selectionRepository.GetByIdAsync(request.SelectionId);

            candidate = selection.Candidate;
        }

        var vacancyResponses = await _vacancyResponseRepository.GetByCandidateIdAsync(candidate.Id);

        var student = await _studentRepository.GetStudentByIdAsync(candidate.StudentId);

        if (request.Roles.Contains("Curator"))
        {
            var curator = await _curatorRepository.GetCuratorByUserId(request.UserId);

            if (curator == null) throw new Forbidden("You don't have access to the curator");

            var companyVacancyIds = (await _vacancyRepository
                    .GetByCompanyAsync(curator.Company.Id))
                .Select(v => v.Id)
                .ToList();

            if (!selection.Offer.HasValue || !companyVacancyIds.Contains(selection.Offer.Value))
                throw new Forbidden("You don't have access to the selection");
        }
        else if (student.UserId != request.UserId && !request.Roles.Contains("DeanMember"))
            throw new Forbidden("You do not have access to this candidate.");

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

        var selectionDto = new SelectionDto
        {
            Id = selection.Id,
            IsDeleted = selection.IsDeleted,
            DeadLine = selection.DeadLine,
            SelectionStatus = selection.SelectionStatus,
            Candidate = candidateDto,
            Responses = vacanciesDtos
        };

        if (selection.Offer.HasValue)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(selection.Offer.Value);
            var position = await _positionRepository.GetByIdAsync(vacancy.PositionId);
            var company = await _companyRepository.GetByIdAsync(vacancy.CompanyId);


            selectionDto.Offer = new OfferDto
            {
                Position = position.Name,
                CompanyName = company.Name,
            };
        }

        return selectionDto;
    }
}
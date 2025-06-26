using AutoMapper;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using Shared.Contracts.Configs;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Queries;

public class GetVacanciesQueryHandler : IRequestHandler<GetVacanciesQuery, VacanciesDto>
{
    private readonly int _size;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICompanyRepository _companyRepository;
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IPositionRepository _positionRepository;

    public GetVacanciesQueryHandler(IMapper mapper, IVacancyRepository vacancyRepository,
        IOptions<PaginationConfig> config, ICompanyRepository companyRepository, IPositionRepository positionRepository,
        IMediator mediator)
    {
        _mapper = mapper;
        _vacancyRepository = vacancyRepository;
        _companyRepository = companyRepository;
        _positionRepository = positionRepository;
        _mediator = mediator;
        _size = config.Value.PageSize;
    }

    public async Task<VacanciesDto> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
    {
        if (request.Page <= 0) throw new BadRequest("Page must be greater than 0");

        var skip = (request.Page - 1) * _size;

        var vacancies = request.IsArchived
            ? await _vacancyRepository.ListAllArchivedAsync()
            : await _vacancyRepository.ListAllActiveAsync();

        if (request.Roles.Contains("Curator"))
        {
            var curator = await _mediator.Send(new GetCuratorQuery(request.UserId), cancellationToken);
            vacancies = vacancies.Where(x => x.CompanyId == curator.Company.Id);
        }
        else
        {
            if (request.CompanyId.HasValue) vacancies = vacancies.Where(x => x.CompanyId == request.CompanyId.Value);
            if (request.PositionId.HasValue) vacancies = vacancies.Where(x => x.PositionId == request.PositionId.Value);
        }

        vacancies = vacancies.Where(x => x.IsDeleted == request.IsArchived);
        vacancies = vacancies.Where(x => x.IsClosed == request.IsClosed);

        var totalCount = await vacancies.CountAsync(cancellationToken);
        var totalPages = Math.Max(1, (int)Math.Ceiling((double)totalCount / _size));

        if (request.Page > totalPages) throw new BadRequest("Page must be less than or equal to the number of items");

        var pagedVacancies = await vacancies
            .Skip(skip)
            .Take(_size)
            .ToListAsync(cancellationToken);


        var dtos = new List<ListedVacancyDto>();

        foreach (var vacancy in pagedVacancies)
        {
            dtos.Add(new ListedVacancyDto
            {
                Id = vacancy.Id,
                Title = vacancy.Title,
                Position = _mapper.Map<PositionDto>(await _positionRepository.GetByIdAsync(vacancy.PositionId)),
                Company = _mapper.Map<ShortenCompanyDto>(await _companyRepository.GetByIdAsync(vacancy.CompanyId)),
                IsClosed = vacancy.IsClosed,
                IsDeleted = vacancy.IsDeleted,
            });
        }

        return new VacanciesDto(dtos, _size, totalPages, request.Page);
    }
}
using AutoMapper;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using Shared.Contracts.Configs;

namespace SelectionModule.Application.Features.Queries;

public class GetVacanciesQueryHandler : IRequestHandler<GetVacanciesQuery, VacanciesDto>
{
    private readonly int _size;
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly IVacancyRepository _vacancyRepository;

    public GetVacanciesQueryHandler(IMapper mapper, IVacancyRepository vacancyRepository,
        IOptions<PaginationConfig> config, ICompanyRepository companyRepository)
    {
        _mapper = mapper;
        _vacancyRepository = vacancyRepository;
        _companyRepository = companyRepository;
        _size = config.Value.PageSize;
    }

    public async Task<VacanciesDto> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.Page - 1) * _size;

        var vacancies = request.IsArchived
            ? await _vacancyRepository.ListAllArchivedAsync()
            : await _vacancyRepository.ListAllAsync();

        if (request.CompanyId != null) vacancies = vacancies.Where(x => x.CompanyId == request.CompanyId);
        if (request.PositionId != null) vacancies = vacancies.Where(x => x.PositionId == request.PositionId);

        vacancies = vacancies.Where(x => x.IsDeleted == request.IsArchived);
        vacancies = vacancies.Where(x => x.IsClosed == request.IsClosed);

        var totalCount = await vacancies.CountAsync(cancellationToken);

        var pagedVacancies = await vacancies
            .Skip(skip)
            .Take(_size)
            .ToListAsync(cancellationToken);


        var dtos = new List<ListedVacancyDto>();

        foreach (var vacancy in pagedVacancies)
        {
            dtos.Add(new ListedVacancyDto
            {
                Title = vacancy.Title,
                Position = _mapper.Map<PositionDto>(vacancy.Position),
                Company = _mapper.Map<ShortenCompanyDto>(await _companyRepository.GetByIdAsync(vacancy.CompanyId)),
                IsClosed = vacancy.IsClosed,
            });
        }
        
        return new VacanciesDto(dtos, _size, totalCount, request.Page);
    }
}
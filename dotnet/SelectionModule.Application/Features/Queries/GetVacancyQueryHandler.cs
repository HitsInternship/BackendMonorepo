using AutoMapper;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Queries;

public class GetVacancyQueryHandler : IRequestHandler<GetVacancyQuery, VacancyDto>
{
    private readonly IMapper _mapper;
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IVacancyResponseRepository _vacancyResponseRepository;

    public GetVacancyQueryHandler(IMapper mapper, IVacancyRepository vacancyRepository,
        ICompanyRepository companyRepository, IPositionRepository positionRepository,
        IVacancyResponseRepository vacancyResponseRepository)
    {
        _mapper = mapper;
        _vacancyRepository = vacancyRepository;
        _companyRepository = companyRepository;
        _positionRepository = positionRepository;
        _vacancyResponseRepository = vacancyResponseRepository;
    }

    public async Task<VacancyDto> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
    {
        if (!await _vacancyRepository.CheckIfExistsAsync(request.VacancyId))
            throw new NotFound("Vacancy not found");

        var vacancy = await _vacancyRepository.GetByIdAsync(request.VacancyId);

        var vacancyDto = _mapper.Map<VacancyDto>(vacancy);

        vacancyDto.Position = _mapper.Map<PositionDto>(await _positionRepository.GetByIdAsync(vacancy.PositionId));
        vacancyDto.Company = _mapper.Map<ShortenCompanyDto>(await _companyRepository.GetByIdAsync(vacancy.CompanyId));
        vacancyDto.HasResponse = await _vacancyResponseRepository.CheckIfExistsByUserIdAsync(request.UserId);

        return vacancyDto;
    }
}
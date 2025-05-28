using AutoMapper;
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

    public GetVacancyQueryHandler(IMapper mapper, IVacancyRepository vacancyRepository)
    {
        _mapper = mapper;
        _vacancyRepository = vacancyRepository;
    }

    public async Task<VacancyDto> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
    {
        if(!await _vacancyRepository.CheckIfExistsAsync(request.VacancyId))
            throw new NotFound("Vacancy not found");
        
        var vacancy = await _vacancyRepository.GetByIdAsync(request.VacancyId);
        
        return _mapper.Map<VacancyDto>(vacancy);
    }
}
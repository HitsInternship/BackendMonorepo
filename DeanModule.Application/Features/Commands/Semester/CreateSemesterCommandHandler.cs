using AutoMapper;
using DeanModule.Contracts.Commands.Semester;
using DeanModule.Contracts.Repositories;
using MediatR;

namespace DeanModule.Application.Features.Commands.Semester;

public class CreateSemesterCommandHandler : IRequestHandler<CreateSemesterCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ISemesterRepository _semesterRepository;

    public CreateSemesterCommandHandler(IMapper mapper, ISemesterRepository semesterRepository)
    {
        _mapper = mapper;
        _semesterRepository = semesterRepository;
    }

    public async Task<Unit> Handle(CreateSemesterCommand request, CancellationToken cancellationToken)
    {
        await _semesterRepository.AddAsync(_mapper.Map<Domain.Entities.SemesterEntity>(request.SemesterRequestDto));

        return Unit.Value;
    }
}
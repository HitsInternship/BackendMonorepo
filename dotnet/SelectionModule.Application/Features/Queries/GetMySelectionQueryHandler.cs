using CompanyModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Queries;

public class GetMySelectionQueryHandler : IRequestHandler<GetMySelectionQuery, SelectionDto>
{
    private readonly ISender _mediator;
    private readonly IUserRepository _userRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ISelectionRepository _selectionRepository;

    public GetMySelectionQueryHandler(IStudentRepository studentRepository, ICandidateRepository candidateRepository,
        IPositionRepository positionRepository, ICompanyRepository companyRepository, IUserRepository userRepository,
        ISelectionRepository selectionRepository, ISender mediator)
    {
        _studentRepository = studentRepository;
        _candidateRepository = candidateRepository;
        _positionRepository = positionRepository;
        _companyRepository = companyRepository;
        _userRepository = userRepository;
        _selectionRepository = selectionRepository;
        _mediator = mediator;
    }

    public async Task<SelectionDto> Handle(GetMySelectionQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        var student = await _studentRepository.GetStudentByUserIdAsync(request.UserId);

        return await _mediator.Send(new GetSelectionQuery(Guid.Empty, user.Id, ["Student"], student.Id),
            cancellationToken);
    }
}
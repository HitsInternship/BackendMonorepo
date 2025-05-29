using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Commands;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace CompanyModule.Application.Handlers.Company
{
    public class AddCuratorCommandHandler : IRequestHandler<AddCuratorCommand, Curator>
    {
        private readonly ICuratorRepository _curatorRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AddCuratorCommandHandler(ICuratorRepository curatorRepository, ICompanyRepository companyRepository, IMediator mediator, IMapper mapper)
        {
            _curatorRepository = curatorRepository;
            _companyRepository = companyRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Curator> Handle(AddCuratorCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Company company = await _companyRepository.GetByIdAsync(command.companyId);

            Curator curator = _mapper.Map<Curator>(command.createRequest);
            curator.Company = company;

            if (command.createRequest.userRequest != null && command.createRequest.userId != null)
            {
                throw new BadRequest("Provide userRequest or userId and not both");
            }

            Guid userId;
            if (command.createRequest.userRequest != null)
            {
                userId = (await _mediator.Send(new CreateUserCommand(command.createRequest.userRequest))).Id;
            }
            else if (command.createRequest.userId != null) userId = (Guid)command.createRequest.userId;
            else throw new BadRequest("Provide userRequest or userId");

            curator.UserId = userId;
            curator.User = await _mediator.Send(new AddUserRoleCommand(userId, RoleName.Curator));

            await _curatorRepository.AddAsync(curator);

            return curator;
        }
    }
}

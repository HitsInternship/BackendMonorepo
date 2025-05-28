using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Commands;
using UserModule.Contracts.Queries;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace CompanyModule.Application.Handlers.Company
{
    public class AddCompanyPersonCommandHandler : IRequestHandler<AddCompanyPersonCommand, CompanyPerson>
    {
        private readonly ICompanyPersonRepository _companyPersonRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AddCompanyPersonCommandHandler(ICompanyPersonRepository companyPersonRepository, ICompanyRepository companyRepository, IMediator mediator, IMapper mapper)
        {
            _companyPersonRepository = companyPersonRepository;
            _companyRepository = companyRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CompanyPerson> Handle(AddCompanyPersonCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Company company = await _companyRepository.GetByIdAsync(command.companyId);

            CompanyPerson companyPerson = command.createRequest.isCurator ? _mapper.Map<Curator>(command.createRequest) : _mapper.Map<CompanyRepresenter>(command.createRequest);
            companyPerson.Company = company;

            User user = null;
            if (command.createRequest.userRequest != null)
            {
                user = await _mediator.Send(new CreateUserCommand(command.createRequest.userRequest));
            }
            else if (command.createRequest.userId == null)
            {
                throw new BadRequest("Provide userRequest or userId");
            }

            user = await _mediator.Send(new AddUserRoleCommand(command.createRequest.userId ?? user!.Id, command.createRequest.isCurator ? RoleName.Curator : RoleName.CompanyRepresenter));

            companyPerson.UserId = user.Id;
            companyPerson.User = user;

            await _companyPersonRepository.AddAsync(companyPerson);

            return companyPerson;
        }
    }
}

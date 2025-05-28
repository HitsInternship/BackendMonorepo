using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Queries;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace CompanyModule.Application.Handlers.Company
{
    public class GetCompanyPersonQueryHandler : IRequestHandler<GetCompanyPersonQuery, CompanyPerson>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyPersonRepository _companyPersonRepository;
        private readonly IMediator _mediator;
        public GetCompanyPersonQueryHandler(ICompanyRepository companyRepository, ICompanyPersonRepository companyPersonRepository, IMediator mediator)
        {
            _companyRepository = companyRepository;
            _companyPersonRepository = companyPersonRepository;
            _mediator = mediator;

        }

        public async Task<CompanyPerson> Handle(GetCompanyPersonQuery query, CancellationToken cancellationToken)
        {
            CompanyPerson companyPerson = (await _companyPersonRepository.ListAllAsync()).Where(person => person.UserId == query.personId).FirstOrDefault();
            if (companyPerson == null) throw new NotFound("No company with such user Id");
            User user = await _mediator.Send(new GetUserInfoQuery(query.personId));

            companyPerson.User = user;
            
            return companyPerson;
        }
    }
}

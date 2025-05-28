using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using UserModule.Contracts.Queries;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace CompanyModule.Application.Handlers.Company
{
    public class GetCompanyPersonsQueryHandler : IRequestHandler<GetCompanyPersonsQuery, List<CompanyPerson>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyPersonRepository _companyPersonRepository;
        private readonly IMediator _mediator;
        public GetCompanyPersonsQueryHandler(ICompanyRepository companyRepository, ICompanyPersonRepository companyPersonRepository, IMediator mediator)
        {
            _companyRepository = companyRepository;
            _companyPersonRepository = companyPersonRepository;
            _mediator = mediator;

        }

        public async Task<List<CompanyPerson>> Handle(GetCompanyPersonsQuery query, CancellationToken cancellationToken)
        {
            Domain.Entities.Company company = await _companyRepository.GetByIdAsync(query.companyId);
            List<CompanyPerson> companyPersons = await _companyPersonRepository.GetCompanyPersonsByCompany(company, query.includeCurators, query.includeRepresenters);

            List<Guid> userIds = companyPersons.Select(companyPerson => companyPerson.UserId).ToList();
            List<User> users = await _mediator.Send(new GetListUserQuery(userIds));

            foreach (CompanyPerson companyPerson in companyPersons)
            {
                companyPerson.User = users.First(user => user.Id == companyPerson.UserId);
            }
            
            return companyPersons;
        }
    }
}

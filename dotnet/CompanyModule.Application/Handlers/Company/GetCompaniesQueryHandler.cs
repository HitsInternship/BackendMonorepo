using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using MediatR;

namespace CompanyModule.Application.Handlers.Company
{
    public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, List<Domain.Entities.Company>>
    {
        private readonly ICompanyRepository _companyRepository;
        public GetCompaniesQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<Domain.Entities.Company>> Handle(GetCompaniesQuery query, CancellationToken cancellationToken)
        {
            return (await _companyRepository.GetAllAsync())!.ToList();
        }
    }
}

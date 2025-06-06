using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using UserModule.Contracts.Queries;
using UserModule.Domain.Entities;

namespace CompanyModule.Application.Handlers.CompanyPart
{
    public class GetCuratorsQueryHandler : IRequestHandler<GetCuratorsQuery, List<Curator>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICuratorRepository _curatorRepository;
        private readonly IMediator _mediator;
        public GetCuratorsQueryHandler(ICompanyRepository companyRepository, ICuratorRepository curatorRepository, IMediator mediator)
        {
            _companyRepository = companyRepository;
            _curatorRepository = curatorRepository;
            _mediator = mediator;

        }

        public async Task<List<Curator>> Handle(GetCuratorsQuery query, CancellationToken cancellationToken)
        {
            Domain.Entities.Company company = await _companyRepository.GetByIdAsync(query.companyId);
            List<Curator> curators = await _curatorRepository.GetCuratorsByCompany(company);

            List<Guid> userIds = curators.Select(curator => curator.UserId).ToList();
            List<User> users = await _mediator.Send(new GetListUserQuery(userIds));

            foreach (Curator companyPerson in curators)
            {
                companyPerson.User = users.First(user => user.Id == companyPerson.UserId);
            }
            
            return curators;
        }
    }
}

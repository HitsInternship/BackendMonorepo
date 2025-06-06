using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using MediatR;

namespace CompanyModule.Application.Handlers.CompanyPart
{
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, Domain.Entities.Company>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public AddCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Company> Handle(AddCompanyCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Company company = _mapper.Map<Domain.Entities.Company>(command.createRequest);

            await _companyRepository.AddAsync(company);

            return company;
        }
    }
}

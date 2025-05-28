using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using MediatR;

namespace CompanyModule.Application.Handlers.Company
{
    public class EditCompanyCommandHandler : IRequestHandler<EditCompanyCommand, Domain.Entities.Company>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public EditCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Company> Handle(EditCompanyCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Company company = await _companyRepository.GetByIdAsync(command.companyId);
            _mapper.Map(command.editRequest, company);

            await _companyRepository.UpdateAsync(company);

            return company;
        }
    }
}

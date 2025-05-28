using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using MediatR;

namespace CompanyModule.Application.Handlers.Company
{
    public class ChangeCompanyStatusCommandHandler : IRequestHandler<ChangeCompanyStatusCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;
        public ChangeCompanyStatusCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Unit> Handle(ChangeCompanyStatusCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Company company = await _companyRepository.GetByIdAsync(command.companyId);

            company.Status = command.companyStatus;
            await _companyRepository.UpdateAsync(company);

            return Unit.Value;
        }
    }
}

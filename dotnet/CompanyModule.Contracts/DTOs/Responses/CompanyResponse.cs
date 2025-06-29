using CompanyModule.Domain.Enums;

namespace CompanyModule.Contracts.DTOs.Responses
{
    public class CompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CompanyStatus Status { get; set; }

        public CompanyResponse() {}
    }
}

using CompanyModule.Domain.Enums;

namespace CompanyModule.Contracts.DTOs.Responses
{
    public class CompanyResponse
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public CompanyStatus status { get; set; }

        public CompanyResponse() {}
    }
}

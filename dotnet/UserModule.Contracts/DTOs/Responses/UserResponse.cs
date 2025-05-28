using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace UserModule.Contracts.DTOs.Responses
{
    public class UserResponse
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public List<RoleName> roles { get; set; }

        public UserResponse() { }
    }
}

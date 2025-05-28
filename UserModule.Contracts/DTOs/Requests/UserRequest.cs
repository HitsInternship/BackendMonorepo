using Shared.Extensions.Validation;
using System.ComponentModel.DataAnnotations;
using UserModule.Domain.Enums;

namespace UserModule.Contracts.DTOs.Requests
{
    public class UserRequest
    {
        public string name { get; set; }
        public string surname { get; set; }

        [DataType(DataType.EmailAddress)]
        [Annotations.Email]
        public string email { get; set; }

        public UserRequest() {}
    }

    public class SearchUserRequest
    {
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? email { get; set; }
        public List<RoleName>? roles { get; set; }

        public SearchUserRequest() { }
    }
}

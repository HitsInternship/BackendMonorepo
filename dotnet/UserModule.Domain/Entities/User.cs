using Shared.Domain.Entites;
using UserModule.Domain.Enums;

namespace UserModule.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();

        public User() {}
    }
}

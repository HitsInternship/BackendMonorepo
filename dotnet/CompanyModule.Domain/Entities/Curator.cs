using Shared.Domain.Entites;
using System.ComponentModel.DataAnnotations.Schema;
using UserModule.Domain.Entities;

namespace CompanyModule.Domain.Entities
{
    public class Curator : BaseEntity
    {
        public Guid UserId { get; set; }
        [NotMapped] 
        public User User { get; set; }
        public string Telegram { get; set; }
        public string Phone { get; set; }
        public Company Company { get; set; }

        public Curator() {}
    }
}

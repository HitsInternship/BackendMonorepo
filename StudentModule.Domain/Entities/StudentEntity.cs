using Shared.Domain.Entites;
using StudentModule.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using UserModule.Domain.Entities;

namespace StudentModule.Domain.Entities
{
    public class StudentEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        [NotMapped]
        public User User { get; set; }
        public string? Middlename { get; set; }
        public string? Phone { get; set; }
        public bool IsHeadMan { get; set; }
        public StudentStatus Status { get; set; }

        public Guid GroupId { get; set; }
        public GroupEntity Group { get; set; }
    }
}

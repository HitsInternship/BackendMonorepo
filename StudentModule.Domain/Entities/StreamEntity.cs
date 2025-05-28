using Shared.Domain.Entites;
using StudentModule.Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace StudentModule.Domain.Entities
{
    public class StreamEntity : BaseEntity
    {
        public int StreamNumber { get; set; }
        public int Year { get; set; }
        [Range(1, 4, ErrorMessage = "Invalid course value")]
        public int Course { get; set; }
        public StreamStatus Status { get; set; }

        public ICollection<GroupEntity> Groups { get; set; } = new List<GroupEntity>();
    }
}

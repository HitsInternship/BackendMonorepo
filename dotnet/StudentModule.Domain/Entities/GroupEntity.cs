using Shared.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Domain.Entities
{
    public class GroupEntity : BaseEntity
    {
        public int GroupNumber { get; set; }

        public ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();

        public Guid StreamId { get; set; }
        public StreamEntity Stream { get; set; }
    }
}

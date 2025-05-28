using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Entites;

namespace DeanModule.Domain.Entities;

public class StreamSemesterEntity : BaseEntity
{
    public Guid StreamId { get; set; }

    [ForeignKey("SemesterEntity")]
    public Guid SemesterId { get; set; }
    
    [Required]
    public int Number { get; set; }

    [Required]
    public required SemesterEntity SemesterEntity { get; set; }
}
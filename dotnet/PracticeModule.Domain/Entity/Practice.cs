using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Entites;
using StudentModule.Contracts.DTOs;
using StudentModule.Domain.Entities;
using CompanyModule.Domain.Entities;
using SelectionModule.Domain.Entites;

namespace PracticeModule.Domain.Entity;

public class Practice : BaseEntity
{
    public GlobalPractice GlobalPractice { get; set; }

    public Guid StudentId { get; set; }
    [NotMapped]
    public StudentEntity Student { get; set; }

    public Guid CompanyId { get; set; }
    [NotMapped]
    public Company Company { get; set; }
    [NotMapped]
    public Company NewCompany { get; set; }

    public Guid PositionId { get; set; }
    [NotMapped]
    public PositionEntity Position { get; set; }
    [NotMapped]
    public PositionEntity NewPosition { get; set; }

    public int? Mark { get; set; }
    public StudentPracticeCharacteristic? StudentPracticeCharacteristics { get; set; }
    public PracticeDiary? PracticeDiary { get; set; }

    public Practice() {}
}
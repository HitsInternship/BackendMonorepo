using Shared.Domain.Entites;

namespace DeanModule.Domain.Entities;

public class DeanMember : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
}
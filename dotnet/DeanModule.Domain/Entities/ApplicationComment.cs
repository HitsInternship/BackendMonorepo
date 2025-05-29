using Shared.Domain.Entites;

namespace DeanModule.Domain.Entities;

public class ApplicationComment : Comment
{
    public ApplicationEntity Application { get; set; }
}
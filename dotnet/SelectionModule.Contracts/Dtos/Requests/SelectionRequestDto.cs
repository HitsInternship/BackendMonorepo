namespace SelectionModule.Contracts.Dtos.Requests;

public record SelectionRequestDto
{
    public Guid SemesterId { get; set; }
    public Guid StreamId { get; set; }
    public DateOnly? Deadline { get; set; }
}
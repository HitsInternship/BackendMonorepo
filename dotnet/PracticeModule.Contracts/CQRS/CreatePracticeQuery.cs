using MediatR;
using PracticeModule.Contracts.Model;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.CQRS;

public class CreatePracticeQuery: IRequest<PracticeDTO>
{
    public Guid StudentId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid SemesterId { get; set; }
    public Guid PositionId { get; set; }
    public int Mark { get; set; }
    public PracticeType PracticeType { get; set; }
}
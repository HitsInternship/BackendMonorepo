using MediatR;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.Queries;

public class GetPracticeDiaryByIdQuery : IRequest<PracticeDiary>
{
    public Guid Id { get; set; }
}
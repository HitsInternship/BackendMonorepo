using MediatR;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.CQRS;

public class GetAllPracticeDiariesQuery : IRequest<List<PracticeDiary>>
{
}
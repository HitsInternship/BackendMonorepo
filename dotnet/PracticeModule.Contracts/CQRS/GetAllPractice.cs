using MediatR;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.CQRS;

public class GetAllPractice : IRequest<List<Practice>>
{
}
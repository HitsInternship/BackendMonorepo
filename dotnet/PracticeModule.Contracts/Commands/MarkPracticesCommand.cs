using MediatR;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.Commands
{
    public record MarkPracticesCommand(Guid practiceId, int mark) : IRequest<Unit>;
}
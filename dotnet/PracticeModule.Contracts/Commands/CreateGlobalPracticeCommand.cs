using MediatR;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.Commands
{
    public record CreateGlobalPracticeCommand(CreateGlobalPracticeRequest createRequest) : IRequest<GlobalPractice>;
}
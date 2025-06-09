using MediatR;
using Microsoft.AspNetCore.Http;

namespace PracticeModule.Contracts.Commands;

public class StudentCharacteristicsAddCommand : IRequest
{
    public Guid IdPractice { get; set; }
    public IFormFile? FormPhoto { get; set; }
}
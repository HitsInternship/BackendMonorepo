using MediatR;
using Microsoft.AspNetCore.Http;

namespace PracticeModule.Contracts.CQRS;

public class StudentCharacteristicsAddQuery : IRequest
{ 
    public Guid IdPractice { get; set; }
    public IFormFile? FormPhoto { get; set; }
}
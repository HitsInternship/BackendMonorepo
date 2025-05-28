using MediatR;
using PracticeModule.Contracts.CQRS;
using PracticeModule.Contracts.Model;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;

namespace PracticeModule.Application.Handler;

public class CreatePracticeHandler : IRequestHandler<CreatePracticeQuery, PracticeDTO>
{
    private readonly PracticeDbContext _context;
    
    public CreatePracticeHandler(PracticeDbContext context)
    {
        _context = context;
    }
    
    public async Task<PracticeDTO> Handle(CreatePracticeQuery request, CancellationToken cancellationToken)
    {
        
        var practice = new Practice()
        {
            Id = Guid.NewGuid(),
            StudentId = request.StudentId,
            PositionId = request.PositionId,
            CompanyId = request.CompanyId,
            SemesterId = request.SemesterId,
            Mark = request.Mark,
            PracticeType = request.PracticeType
        };

        _context.Practice.Add(practice);
        await _context.SaveChangesAsync();
        
        
        var practiceDto = new PracticeDTO()
        {
            Id = practice.Id,
            StudentId = practice.StudentId,
            PositionId = practice.PositionId,
            CompanyId = practice.CompanyId,
            SemesterId = practice.SemesterId,
            Mark = practice.Mark,
            PracticeType = practice.PracticeType
        };
        return practiceDto;
    }
}
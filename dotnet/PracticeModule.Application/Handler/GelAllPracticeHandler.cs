using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.CQRS;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;

namespace PracticeModule.Application.Handler;

public class GelAllPracticeHandler : IRequestHandler<GetAllPractice, List<Practice>>
{
    
    private readonly PracticeDbContext  _context;

    public GelAllPracticeHandler(PracticeDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Practice>> Handle(GetAllPractice request, CancellationToken cancellationToken)
    {
        return await _context.Practice.ToListAsync();
    }
}
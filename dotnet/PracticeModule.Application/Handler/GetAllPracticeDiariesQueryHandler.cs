using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Queries;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;

namespace PracticeModule.Application.Handler;

public class GetAllPracticeDiariesQueryHandler : IRequestHandler<GetAllPracticeDiariesQuery, List<PracticeDiary>>
{
    private readonly PracticeDbContext _context;

    public GetAllPracticeDiariesQueryHandler(PracticeDbContext context)
    {
        _context = context;
    }

    public async Task<List<PracticeDiary>> Handle(GetAllPracticeDiariesQuery request, CancellationToken cancellationToken)
    {
        return await _context.PracticeDiary
            .Include(x => x.Comment)
            .ToListAsync(cancellationToken);
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Queries;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;

namespace PracticeModule.Application.Handler;

public class GetPracticeDiaryByIdQueryHandler : IRequestHandler<GetPracticeDiaryByIdQuery, PracticeDiary>
{
    private readonly PracticeDbContext _context;

    public GetPracticeDiaryByIdQueryHandler(PracticeDbContext context)
    {
        _context = context;
    }

    public async Task<PracticeDiary> Handle(GetPracticeDiaryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.PracticeDiary
            .Include(x => x.Comment)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}
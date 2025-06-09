using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Queries;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;

namespace PracticeModule.Application.Handler;

public class GetAllStudentCharacteristicsQueryHandler : IRequestHandler<GetAllStudentCharacteristicsQuery, List<StudentPracticeCharacteristic>>
{
    private readonly PracticeDbContext _context;

    public GetAllStudentCharacteristicsQueryHandler(PracticeDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudentPracticeCharacteristic>> Handle(GetAllStudentCharacteristicsQuery request, CancellationToken cancellationToken)
    {
        return await _context.StudentPracticeCharacteristic
            .Include(x => x.PracticeComment)
            .ToListAsync(cancellationToken);
    }
}
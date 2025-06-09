using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Queries;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;

namespace PracticeModule.Application.Handler;

public class GetStudentCharacteristicByIdQueryHandler : IRequestHandler<GetStudentCharacteristicByIdQuery, StudentPracticeCharacteristic>
{
    private readonly PracticeDbContext _context;

    public GetStudentCharacteristicByIdQueryHandler(PracticeDbContext context)
    {
        _context = context;
    }

    public async Task<StudentPracticeCharacteristic> Handle(GetStudentCharacteristicByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.StudentPracticeCharacteristic
            .Include(x => x.PracticeComment)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}

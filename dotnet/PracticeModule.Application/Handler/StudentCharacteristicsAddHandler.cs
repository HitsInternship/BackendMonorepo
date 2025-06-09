using DocumentModule.Contracts.Commands;
using DocumentModule.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Commands;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;
using Shared.Domain.Exceptions;

namespace PracticeModule.Application.Handler;

public class StudentCharacteristicsAddHandler : IRequestHandler<StudentCharacteristicsAddCommand>
{
    private readonly PracticeDbContext _context;
    private readonly IMediator _mediator;
    public StudentCharacteristicsAddHandler(PracticeDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task Handle(StudentCharacteristicsAddCommand request, CancellationToken cancellationToken)
    {
        var getPractice = await _context.Practice.FirstOrDefaultAsync(x => x.Id == request.IdPractice, cancellationToken);
        if (getPractice == null)
        {
            throw new NotFound("Practice does not exist");
        }
        var command = new LoadDocumentCommand(DocumentType.StudentPracticeCharacteristic, request.FormPhoto);
        var id = await _mediator.Send(command, cancellationToken);

        _context.StudentPracticeCharacteristic.Add(new StudentPracticeCharacteristic()
        {
            
            Id = Guid.NewGuid(),
            DocumentId = id,
            PracticeId = request.IdPractice
        });
        
        await _context.SaveChangesAsync();
    }
}
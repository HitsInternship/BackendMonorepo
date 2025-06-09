using DocumentModule.Contracts.Commands;
using DocumentModule.Contracts.Queries;
using DocumentModule.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Commands;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;
using Shared.Domain.Exceptions;

namespace PracticeModule.Application.Handler;

public class PracticeDiaryAddHandler :  IRequestHandler<PracticeDiaryAddCommand>
{
    
    private readonly PracticeDbContext _context;
    private readonly IMediator _mediator;
    public PracticeDiaryAddHandler(PracticeDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task Handle(PracticeDiaryAddCommand request, CancellationToken cancellationToken)
    {
        var getPractice = await _context.Practice.FirstOrDefaultAsync(x => x.Id == request.IdPractice, cancellationToken);
        if (getPractice == null)
        {
            throw new NotFound("Practice does not exist");
        }
        var command = new LoadDocumentCommand(DocumentType.PracticeDiary, request.FormPhoto);
        var id = await _mediator.Send(command, cancellationToken);

        _context.PracticeDiary.Add(new PracticeDiary()
        {
            
            Id = Guid.NewGuid(),
            DocumentId = id,
            PracticeId = request.IdPractice
        });
        
        await _context.SaveChangesAsync();
    }
}
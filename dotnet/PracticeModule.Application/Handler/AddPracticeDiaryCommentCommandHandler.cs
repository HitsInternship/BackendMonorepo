using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.CQRS;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;

namespace PracticeModule.Application.Handler;

public class AddPracticeDiaryCommentCommandHandler : IRequestHandler<AddPracticeDiaryCommentCommand>
{
    private readonly PracticeDbContext _context;

    public AddPracticeDiaryCommentCommandHandler(PracticeDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddPracticeDiaryCommentCommand request, CancellationToken cancellationToken)
    {
        var diary = await _context.PracticeDiary
            .Include(d => d.Comment)
            .FirstOrDefaultAsync(d => d.Id == request.DiaryId, cancellationToken);

        if (diary == null)
        {
            throw new Exception("Practice diary not found");
        }

        var comment = new PracticeDiaryComment
        {
            Comment = request.Comment,
            DiaryId = request.DiaryId
        };

        diary.Comment ??= new List<PracticeDiaryComment>();
        diary.Comment.Add(comment);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
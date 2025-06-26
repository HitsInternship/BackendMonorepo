using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Commands;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;

namespace PracticeModule.Application.Handler;

public class AddStudentCharacteristicCommentCommandHandler : IRequestHandler<AddStudentCharacteristicCommentCommand>
{
    private readonly PracticeDbContext _context;

    public AddStudentCharacteristicCommentCommandHandler(PracticeDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddStudentCharacteristicCommentCommand request, CancellationToken cancellationToken)
    {
        var characteristic = await _context.StudentPracticeCharacteristic
            .Include(c => c.PracticeComment)
            .FirstOrDefaultAsync(c => c.Id == request.CharacteristicId, cancellationToken);

        if (characteristic == null)
            throw new Exception("Student characteristic not found");

        var comment = new StudentPracticeCharacteristicComment
        {
            Comment = request.Comment,
            PracticeCharacteristicId = request.CharacteristicId
        };

        characteristic.PracticeComment ??= new List<StudentPracticeCharacteristicComment>();
        characteristic.PracticeComment.Add(comment);

        await _context.StudentPracticeCharacteristicComment.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);    }
}
using MediatR;

namespace PracticeModule.Contracts.CQRS;

public class AddPracticeDiaryCommentCommand : IRequest
{
    public Guid DiaryId { get; set; }
    public string Comment { get; set; }
}
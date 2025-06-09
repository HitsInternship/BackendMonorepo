using MediatR;

namespace PracticeModule.Contracts.Commands;

public class AddPracticeDiaryCommentCommand : IRequest
{
    public Guid DiaryId { get; set; }
    public string Comment { get; set; }
}
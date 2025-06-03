using MediatR;

namespace PracticeModule.Contracts.CQRS;

public class AddStudentCharacteristicCommentCommand : IRequest
{
    public Guid CharacteristicId { get; set; }
    public string Comment { get; set; }
}

using MediatR;

namespace PracticeModule.Contracts.Commands;

public class AddStudentCharacteristicCommentCommand : IRequest
{
    public Guid CharacteristicId { get; set; }
    public string Comment { get; set; }
}



using MediatR;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.CQRS;

public class GetStudentCharacteristicByIdQuery : IRequest<StudentPracticeCharacteristic>
{
    public Guid Id { get; set; }
}
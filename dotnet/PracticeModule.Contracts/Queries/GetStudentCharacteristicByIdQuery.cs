using MediatR;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.Queries;

public class GetStudentCharacteristicByIdQuery : IRequest<StudentPracticeCharacteristic>
{
    public Guid Id { get; set; }
}
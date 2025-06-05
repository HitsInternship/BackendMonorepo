using MediatR;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.CQRS;

public class GetAllStudentCharacteristicsQuery : IRequest<List<StudentPracticeCharacteristic>>
{
}
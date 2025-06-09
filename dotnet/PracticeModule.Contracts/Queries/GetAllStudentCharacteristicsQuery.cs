using MediatR;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.Queries;

public class GetAllStudentCharacteristicsQuery : IRequest<List<StudentPracticeCharacteristic>>
{
}
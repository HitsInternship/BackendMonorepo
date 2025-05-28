using Shared.Domain.Exceptions;

namespace DeanModule.Application.Exceptions;

public class SemesterNotFound(Guid semesterId) : NotFound($"Semester with id {semesterId} was not found.");
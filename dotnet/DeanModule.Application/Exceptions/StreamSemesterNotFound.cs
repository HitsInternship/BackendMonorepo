using Shared.Domain.Exceptions;

namespace DeanModule.Application.Exceptions;

public class StreamSemesterNotFound(Guid semesterId) : NotFound($"StreamSemester with id {semesterId} was not found.");

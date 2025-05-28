using Shared.Domain.Exceptions;

namespace DeanModule.Application.Exceptions;

public class ApplicationNotFound(Guid applicationId) : NotFound($"Application with id {applicationId} was not found.");
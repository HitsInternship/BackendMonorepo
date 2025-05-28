namespace Shared.Domain.Exceptions;

public class NotFound(string? message) : Exception(message);
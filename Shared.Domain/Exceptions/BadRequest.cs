namespace Shared.Domain.Exceptions;

public class BadRequest(string? message) : Exception(message);
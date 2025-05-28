namespace Shared.Domain.Exceptions;

public class Unauthorized(string? message) : Exception(message);
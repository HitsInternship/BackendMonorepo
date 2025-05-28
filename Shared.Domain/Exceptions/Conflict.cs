namespace Shared.Domain.Exceptions;

public class Conflict(string? message) : Exception(message);
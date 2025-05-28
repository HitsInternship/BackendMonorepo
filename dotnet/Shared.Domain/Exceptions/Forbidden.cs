namespace Shared.Domain.Exceptions;

public class Forbidden(string? message) : Exception(message);
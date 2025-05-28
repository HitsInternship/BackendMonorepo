namespace Shared.Contracts.Dtos;

/// <summary>
/// DTO комментария, содержащего текст и информацию об авторе.
/// </summary>
public record CommentDto : BaseDto
{
    /// <summary>
    /// Содержимое комментария.
    /// </summary>
    public string Content { get; init; }

    /// <summary>
    /// Автор комментария.
    /// </summary>
    public CommentUserDto Author { get; init; }
}

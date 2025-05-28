namespace Shared.Contracts.Dtos;

/// <summary>
/// DTO, представляющий информацию о пользователе, оставившем комментарий.
/// </summary>
public class CommentUserDto
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string Surname { get; set; }
}

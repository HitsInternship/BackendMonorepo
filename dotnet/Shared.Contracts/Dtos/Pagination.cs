namespace Shared.Contracts.Dtos;

/// <summary>
/// Параметры пагинации.
/// </summary>
public record Pagination(int Size, int Count, int Current)
{
    /// <summary>
    /// Размер страницы.
    /// </summary>
    public int Size { get; set; } = Size;

    /// <summary>
    /// Общее количество элементов.
    /// </summary>
    public int Count { get; set; } = Count;

    /// <summary>
    /// Текущая страница.
    /// </summary>
    public int Current { get; set; } = Current;
}

using Shared.Contracts.Dtos;

namespace SelectionModule.Contracts.Dtos.Responses;

/// <summary>
/// DTO для представления списка вакансий с информацией о пагинации.
/// </summary>
public class VacanciesDto
{
    /// <summary>
    /// Список вакансий на текущей странице.
    /// </summary>
    public List<ListedVacancyDto> Vacancies { get; set; }

    /// <summary>
    /// Данные пагинации.
    /// </summary>
    public Pagination Pagination { get; set; }

    /// <summary>
    /// Конструктор с параметрами.
    /// </summary>
    /// <param name="vacancies">Список вакансий.</param>
    /// <param name="size">Размер страницы.</param>
    /// <param name="count">Общее количество записей.</param>
    /// <param name="current">Текущая страница.</param>
    public VacanciesDto(List<ListedVacancyDto> vacancies, int size, int count, int current)
    {
        Vacancies = vacancies;
        Pagination = new Pagination(size, count, current);
    }
}
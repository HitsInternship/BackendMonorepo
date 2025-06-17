namespace Shared.Contracts.Configs;

public class DeadlineNotificationOptions
{
    public List<int> DaysBefore { get; set; } = [1, 7];
}
using BackgroundJobs.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Shared.Contracts.Configs;

namespace BackgroundJobs;

public static class DependencyInjection
{
    public static void AddBackGroundJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {
            var jobKey = new JobKey("SendUnsentMessagesTrigger");
            q.AddJob<SendUnsentMessagesJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("SendUnsentMessagesTrigger")
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMinutes(2))
                    .RepeatForever()));
        });
        services.Configure<DeadlineNotificationOptions>(
            configuration.GetSection("DeadlineNotification"));

        services.AddQuartz(q =>
        {
            var jobKey = new JobKey("GlobalSelectionDeadlineJob");

            q.AddJob<SelectionDeadlineJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("GlobalSelectionDeadlineJob-trigger")
                .WithSchedule(CronScheduleBuilder
                    .DailyAtHourAndMinute(0, 0)
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Asia/Novosibirsk"))
                )
            );
        });

        services.AddQuartzHostedService(opt => { opt.WaitForJobsToComplete = true; });
    }
}
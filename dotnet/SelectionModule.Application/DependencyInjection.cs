using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SelectionModule.Application.BackgroundJobs;
using Shared.Contracts.Configs;

namespace SelectionModule.Application;

public static class DependencyInjection
{
    public static void AddSelectionModuleApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(typeof(MappingProfile));

        services.Configure<DeadlineNotificationOptions>(
            configuration.GetSection("DeadlineNotification"));

        services.AddQuartz(q =>
        {
            var jobKey = new JobKey("GlobalSelectionDeadlineJob");

            q.AddJob<SelectionDeadlineJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("GlobalSelectionDeadlineJob-trigger")
                .WithDailyTimeIntervalSchedule(x => x
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Asia/Novosibirsk"))
                )
            );
        });

        services.AddQuartzHostedService(opt => { opt.WaitForJobsToComplete = true; });
    }
}
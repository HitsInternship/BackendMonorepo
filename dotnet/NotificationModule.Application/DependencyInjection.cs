using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace NotificationModule.Application;

public static class DependencyInjection 
{
    public static void AddNotificationModuleApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

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

        services.AddQuartzHostedService();
    }
}
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace NotificationModule.Kafka;

public static class KafkaLogger
{
    public static ProducerBuilder<Null, string> WithLogging(
        this ProducerBuilder<Null, string> builder,
        ILogger logger)
    {
        builder.SetLogHandler((_, logMessage) =>
        {
            switch (logMessage.Level)
            {
                case SyslogLevel.Emergency:
                case SyslogLevel.Alert:
                case SyslogLevel.Critical:
                case SyslogLevel.Error:
                    logger.LogError("[Kafka] {Level}: {Message}", logMessage.Level, logMessage.Message);
                    break;
                case SyslogLevel.Warning:
                    logger.LogWarning("[Kafka] {Level}: {Message}", logMessage.Level, logMessage.Message);
                    break;
                case SyslogLevel.Notice:
                case SyslogLevel.Info:
                    logger.LogInformation("[Kafka] {Level}: {Message}", logMessage.Level, logMessage.Message);
                    break;
                case SyslogLevel.Debug:
                    logger.LogDebug("[Kafka] {Level}: {Message}", logMessage.Level, logMessage.Message);
                    break;
            }
        });

        builder.SetErrorHandler((_, error) =>
        {
            if (error.IsFatal)
                logger.LogCritical("[Kafka] Fatal error: {Reason}", error.Reason);
            else
                logger.LogWarning("[Kafka] Error: {Reason}", error.Reason);
        });

        return builder;
    }

    public static ConsumerBuilder<Ignore, string> WithLogging(
        this ConsumerBuilder<Ignore, string> builder,
        ILogger logger)
    {
        builder.SetLogHandler((_, logMessage) =>
        {
            switch (logMessage.Level)
            {
                case SyslogLevel.Emergency:
                case SyslogLevel.Alert:
                case SyslogLevel.Critical:
                case SyslogLevel.Error:
                    logger.LogError("[Kafka] {Level}: {Message}", logMessage.Level, logMessage.Message);
                    break;
                case SyslogLevel.Warning:
                    logger.LogWarning("[Kafka] {Level}: {Message}", logMessage.Level, logMessage.Message);
                    break;
                case SyslogLevel.Notice:
                case SyslogLevel.Info:
                    logger.LogInformation("[Kafka] {Level}: {Message}", logMessage.Level, logMessage.Message);
                    break;
                case SyslogLevel.Debug:
                    logger.LogDebug("[Kafka] {Level}: {Message}", logMessage.Level, logMessage.Message);
                    break;
            }
        });

        builder.SetErrorHandler((_, error) =>
        {
            if (error.IsFatal)
                logger.LogCritical("[Kafka] Fatal error: {Reason}", error.Reason);
            else
                logger.LogWarning("[Kafka] Error: {Reason}", error.Reason);
        });

        return builder;
    }
}

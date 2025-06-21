using MediatR;
using Microsoft.Extensions.Logging;
using NotificationModule.Contracts.Commands;
using NotificationModule.Contracts.Repositories;
using NotificationModule.Domain.Enums;
using Quartz;

namespace BackgroundJobs.Jobs;

public class SendUnsentMessagesJob : IJob
{
    private readonly ISender _sender;
    private readonly IMessageRepository _messageRepository;
    private readonly ILogger<SendUnsentMessagesJob> _logger;


    public SendUnsentMessagesJob(IMessageRepository messageRepository, ILogger<SendUnsentMessagesJob> logger,
        ISender sender)
    {
        _messageRepository = messageRepository;
        _logger = logger;
        _sender = sender;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("SendUnsentMessagesJob started at {time}", DateTimeOffset.Now);

        var messages = await _messageRepository.GetMessagesByStatusAsync(MessageStatus.error);

        foreach (var message in messages)
        {
            _logger.LogInformation("Resending message for {email}", message.Email);

            await _sender.Send(new ResendMessageCommand(message));
        }

        _logger.LogInformation("SendUnsentMessagesJob completed at {time}", DateTimeOffset.Now);
    }
}
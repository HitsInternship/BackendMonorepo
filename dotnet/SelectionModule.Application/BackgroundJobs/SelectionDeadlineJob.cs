using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationModule.Contracts.Commands;
using NotificationModule.Domain.Enums;
using Quartz;
using SelectionModule.Contracts.Repositories;
using Shared.Contracts.Configs;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.BackgroundJobs;

public class SelectionDeadlineJob : IJob
{
    private readonly IUserRepository _userRepository;
    private readonly IGlobalSelectionRepository _selectionRepository;
    private readonly ISender _mediator;
    private readonly DeadlineNotificationOptions _options;
    private readonly ILogger<SelectionDeadlineJob> _logger;


    public SelectionDeadlineJob(IGlobalSelectionRepository selectionRepository, ISender mediator,
        IUserRepository userRepository, IOptions<DeadlineNotificationOptions> options,
        ILogger<SelectionDeadlineJob> logger)

    {
        _selectionRepository = selectionRepository;
        _mediator = mediator;
        _userRepository = userRepository;
        _logger = logger;
        _options = options.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var targetDates = _options.DaysBefore.Select(offset => today.AddDays(offset)).ToList();

        var selections = await _selectionRepository.GetSelectionsByDeadlinesAsync(targetDates);

        _logger.LogInformation($"Started to send deadline notifications for {targetDates.Count} selection deadlines");

        try
        {
            foreach (var global in selections)
            {
                var userIds = selections
                    .SelectMany(g => g.Selections)
                    .Select(s => s.Candidate.UserId)
                    .Distinct()
                    .ToList();

                var emails = (await _userRepository.GetByIdsAsync(userIds)).Select(x => x.Email).ToList();

                var command = new SendDeadlineMessageCommand(
                    emails,
                    global.EndDate,
                    DeadLineType.selection
                );

                await _mediator.Send(command);
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to send notifications for selections deadlines");
        }


        _logger.LogInformation($"Deadline notifications for selections were successfully send");
    }
}
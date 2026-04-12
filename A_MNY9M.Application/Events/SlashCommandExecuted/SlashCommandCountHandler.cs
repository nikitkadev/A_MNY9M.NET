using Microsoft.Extensions.Logging;

using MediatR;

using A_MNY9M._1_Core.Interfaces;

namespace A_MNY9M._2_Application.Events.SlashCommandExecuted;

public class SlashCommandCountHandler(
    ILogger<SlashCommandCountHandler> logger,
    IGuildMemberMetricRepository metricRepository) : INotificationHandler<SlashCommandExecuted>
{
    public async Task Handle(SlashCommandExecuted notification, CancellationToken cancellationToken)
    {
		try
		{
			await metricRepository.IncrementCommandSentCountAsync(notification.SocketSlashCommand.User.Id);
		}
        catch
        {

        }
    }
}

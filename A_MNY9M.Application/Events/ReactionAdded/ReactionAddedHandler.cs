using Microsoft.Extensions.Logging;

using MediatR;

using A_MNY9M._1_Core.Interfaces;

namespace A_MNY9M._2_Application.Events.ReactionAdded;

public class ReactionAddedHandler(
    ILogger<ReactionAddedHandler> logger,
    IGuildMemberMetricRepository metricRepository) : INotificationHandler<ReactionAdded>
{
    public async Task Handle(ReactionAdded notification, CancellationToken cancellationToken)
    {
		try
		{
			if (notification.Reaction.User.Value.IsBot)
				return;

			await metricRepository.IncrementReactionAddedCountAsync(notification.Reaction.UserId);
		}
		catch
		{

			logger.LogError(
				"");

			return;
		}
    }
}
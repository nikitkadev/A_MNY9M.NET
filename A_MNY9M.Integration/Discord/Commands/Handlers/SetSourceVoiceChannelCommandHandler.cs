using Microsoft.Extensions.DependencyInjection;

using A_MNY9M.Core.Common.Result;
using A_MNY9M.Integration.Services;
using A_MNY9M.Integration.Models.Params;
using A_MNY9M.Infrastructure.Database.Entities.Channels;
using A_MNY9M.Infrastructure.Services.Repositories.Channels.Voice;

namespace A_MNY9M.Integration.Discord.Commands.Handlers;

public sealed class SetSourceVoiceChannelCommandHandler(
    GuildLogService guildLogService,
    IServiceScopeFactory serviceScopeFactory)
{
    public async Task<Result<Guid>> HandleAsync(SetSourceVoiceChannelParams request)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var repository = scope.ServiceProvider
            .GetRequiredService<SourceVoiceChannelRepository>();

        var result = await repository.AddSourceVoiceChannelAsync(
            new SourceVoiceChannelDbEntity()
            {
                DiscordId = request.DiscordId,
                GuildCategoryId = request.DiscordCategoryId,
                InitiatorDiscordId = request.InitiatorDiscordId,
                Name = request.Name,
                SetAt = DateTime.UtcNow
            },
            cancellationToken: default);

        if (result.IsSuccess)
        {
            await guildLogService.SendLogAsync(
                $"Канал с id {result.Value} успешно назначен источником.");
        }

        return result;
    }
}

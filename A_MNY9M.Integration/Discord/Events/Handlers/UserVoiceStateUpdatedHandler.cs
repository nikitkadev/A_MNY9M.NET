using Microsoft.Extensions.DependencyInjection;

using Discord.WebSocket;

using A_MNY9M.Integration.Services;
using A_MNY9M.Infrastructure.Database.Entities.Channels;
using A_MNY9M.Infrastructure.Services.Repositories.Channels.Voice;

namespace A_MNY9M.Integration.Discord.Events.Handlers;

public sealed class UserVoiceStateUpdatedHandler(
    GuildLogService logService,
    IServiceScopeFactory scopeFactory,
    DiscordChannelService discordChannelService)
{
    public async Task HandleAsync(
        SocketUser user,
        SocketVoiceState oldState,
        SocketVoiceState newState)
    {
        await using var scope = scopeFactory.CreateAsyncScope();

        var autoVoiceChannelRepository = scope.ServiceProvider.GetRequiredService<AutoVoiceChannelRepository>();
        var sourceVoiceChannelRepository = scope.ServiceProvider.GetRequiredService<SourceVoiceChannelRepository>();

        var oldChannel = oldState.VoiceChannel;
        var newChannel = newState.VoiceChannel;

        if (oldChannel is not null
            && oldChannel.ConnectedUsers.Count == 0
            && oldChannel.Id != newChannel?.Id)
        {
            var isAutoChannel = await autoVoiceChannelRepository
                .IsAutoVoiceChannelAsync(oldState.VoiceChannel.Id);

            if (isAutoChannel)
            {
                await oldState.VoiceChannel.DeleteAsync();

                await autoVoiceChannelRepository.RemoveAutoVoiceChannelAsync(
                    oldChannel.Id,
                    cancellationToken: default);

                await logService.TrySendLogAsync(
                    $"Канал {oldState.VoiceChannel.Name} " +
                    $"с DiscordId {oldState.VoiceChannel.Id} " +
                    $"был автоматически удален.");
            }
        }

        if (newChannel is not null
           && newChannel.Id != oldChannel?.Id)
        {
            var isSourceChannel = await sourceVoiceChannelRepository
                .IsSourceVoiceChannelAsync(newChannel.Id);

            if (isSourceChannel)
            {
                var settings = await autoVoiceChannelRepository.GetAutoVoiceChannelSettingsAsync(
                    userDiscordId: user.Id);

                var createdChannel = await discordChannelService.CreateAutoVoiceChannelAsync(
                    user.Id,
                    newChannel.CategoryId,
                    settings);

                if (createdChannel is not null && user is SocketGuildUser guildUser)
                {
                    await guildUser.ModifyAsync(option =>
                    {
                        option.ChannelId = createdChannel.Id;
                    });

                    await autoVoiceChannelRepository.AddAutoVoiceChannelAsync(
                        new AutoVoiceChannelDbEntity()
                        {
                            CreatedAt = DateTime.UtcNow,
                            DiscordId = createdChannel.Id,
                            LeaderDiscordId = user.Id
                        });

                    await logService.TrySendLogAsync(
                        $"Пользователь {user.Mention} успешно создал авто-канал " +
                        $"и был перемещен в него.");
                }
            }
        }
    }
}
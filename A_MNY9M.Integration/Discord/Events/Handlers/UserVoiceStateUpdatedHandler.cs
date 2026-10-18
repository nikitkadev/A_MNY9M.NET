using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using Discord.WebSocket;

using A_MNY9M.Integration.Services;
using A_MNY9M.Infrastructure.Database.Entities.Channels;
using A_MNY9M.Infrastructure.Services.Repositories.Channels.Voice;

namespace A_MNY9M.Integration.Discord.Events.Handlers;

public sealed class UserVoiceStateUpdatedHandler(
    ILogger<UserVoiceStateUpdatedHandler> logger,
    GuildLogService logService,
    IServiceScopeFactory scopeFactory,
    DiscordChannelService discordChannelService)
{
    public async Task HandleAsync(
        SocketUser user,
        SocketVoiceState oldState,
        SocketVoiceState newState)
    {
        var oldChannel = oldState.VoiceChannel;
        var newChannel = newState.VoiceChannel;

        if (oldChannel?.Id == newChannel?.Id)
            return;

        try
        {
            await using var scope = scopeFactory.CreateAsyncScope();

            var autoVoiceChannelRepository = scope.ServiceProvider
                .GetRequiredService<AutoVoiceChannelRepository>();

            var sourceVoiceChannelRepository = scope.ServiceProvider
                .GetRequiredService<SourceVoiceChannelRepository>();

            if (oldChannel is not null
                && oldChannel.ConnectedUsers.Count == 0)
            {
                try
                {
                    var isAutoChannel = await autoVoiceChannelRepository
                        .IsAutoVoiceChannelAsync(oldChannel.Id);

                    if (isAutoChannel)
                    {
                        await oldChannel.DeleteAsync();

                        await autoVoiceChannelRepository
                            .RemoveAutoVoiceChannelAsync(
                                oldChannel.Id,
                                cancellationToken: default);

                        await logService.TrySendLogAsync(
                            $"Канал {oldChannel.Name} " +
                            $"с DiscordId {oldChannel.Id} " +
                            $"был автоматически удален.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "Ошибка удаления автолобби {ChannelId} после выхода пользователя {UserId}.",
                        oldChannel.Id,
                        user.Id);
                }
            }

            if (newChannel is not null)
            {
                try
                {
                    var isSourceChannel = await sourceVoiceChannelRepository
                        .IsSourceVoiceChannelAsync(newChannel.Id);

                    if (isSourceChannel)
                    {
                        var settings = await autoVoiceChannelRepository
                            .GetAutoVoiceChannelSettingsAsync(
                                userDiscordId: user.Id);

                        var createdChannel = await discordChannelService
                            .CreateAutoVoiceChannelAsync(
                                user.Id,
                                newChannel.CategoryId,
                                settings);

                        if (createdChannel is not null
                            && user is SocketGuildUser guildUser)
                        {
                            await guildUser.ModifyAsync(option =>
                            {
                                option.ChannelId = createdChannel.Id;
                            });

                            await autoVoiceChannelRepository
                                .AddAutoVoiceChannelAsync(
                                    new AutoVoiceChannelDbEntity
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
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "Ошибка создания автолобби для пользователя {UserId} из источника {SourceChannelId}.",
                        user.Id,
                        newChannel.Id);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка обработки смены голосового канала пользователя {UserId}: {OldChannelId} → {NewChannelId}.",
                user.Id,
                oldChannel?.Id,
                newChannel?.Id);
        }
    }
}
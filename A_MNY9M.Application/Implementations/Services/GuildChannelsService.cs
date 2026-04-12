using A_MNY9M._1_Core.Interfaces;
using A_MNY9M._2_Application.Interfaces.Services;
using Amnyam._1_Domain.Exceptions;
using Amnyam._3_Infrastructure.Interfaces;
using A_MNY9M._1_Core.Constants;
using Amnyam.Shared.JsonProviders;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace A_MNY9M._2_Application.Implementations.Services;

public class GuildChannelsService(
    ILogger<GuildChannelsService> logger,
    IJsonProvidersHub providersHub,
    IDiscordService discordService,
    IRoomSettingsRepository settingsRepository) : IGuildChannelsService
{
    public async Task<SocketGuildChannel> GetGuildChannelByDiscordIdAsync(ulong guildChannelDiscordId)
    {
        if (await discordService.DiscordClient.GetChannelAsync(guildChannelDiscordId) is not SocketGuildChannel channel)
        {
            logger.LogWarning
                ("Канал с DiscordId {GuildChannelDiscordId} не является каналом сервера",
                guildChannelDiscordId);

            throw new GuildChannelNotFoundException(guildChannelDiscordId);
        }

        return channel;
    }
    public async Task<RestVoiceChannel> CreateVoiceChannelAsync(ulong guildMemberDiscordId)
    {
        var guild = discordService.GetSocketGuild();

        var leader = await discordService.GetGuildMemberAsync(guildMemberDiscordId) 
            ?? throw new GuildMemberNotFoundException(guildMemberDiscordId);

        var leaderVoiceRoonSettings = await settingsRepository.GetRoomSettingsByGuildMemberDiscordIdAsync(guildMemberDiscordId);

        var voiceChannel = await guild.CreateVoiceChannelAsync(
            name: $"🔉 | {leaderVoiceRoonSettings.VoiceRoomName ?? MlkAdminConstants.DEFAULT_VOICEROOM_NAME}",
            func: properties =>
            {
                properties.CategoryId = providersHub.GuildConfigProvidersHub.Categories.Lobby.DiscordId;
                properties.UserLimit = leaderVoiceRoonSettings.MembersLimit ?? 0;
                if (!string.IsNullOrWhiteSpace(leaderVoiceRoonSettings.Region))
                {
                    properties.RTCRegion = leaderVoiceRoonSettings.Region;
                }
                properties.PermissionOverwrites = new Overwrite[]
                {
                    new(
                        providersHub.GuildConfigProvidersHub.Roles.GetGuildRoleByKey("PlayersClub").Id,
                        PermissionTarget.Role,
                        new OverwritePermissions(
                            connect: PermValue.Allow,
                            sendMessages: PermValue.Allow,
                            manageChannel: PermValue.Deny)
                    ),
                    new(
                        leader.Id,
                        PermissionTarget.User,
                        new OverwritePermissions(manageChannel: PermValue.Allow)
                    )
                };
            }
        );

        return voiceChannel;
    }
}
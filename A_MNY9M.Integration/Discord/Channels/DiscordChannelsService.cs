using Microsoft.Extensions.Options;

using Discord.WebSocket;

using A_MNY9M.Core.Common;
using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Channels;

public class DiscordChannelsService(
    IOptions<MalenkieGuildOption> malenkieOptions,
    IDiscordClientWrapper clientWrapper) : IDiscordChannelsService
{
    public async Task ClearTemporaryVoiceChannelAsync(
        ulong channelId)
    {
        var temporaryVoiceChannel = clientWrapper.MlkGuild.GetVoiceChannel(channelId);

        if(temporaryVoiceChannel is null)
        {
            return;
        }

        await temporaryVoiceChannel.DeleteAsync();
    }

    public async Task CreateTemporaryVoiceChannelAsync(
        SocketGuildUser leader, 
        ulong? categoryId)
    {
        var temporaryVoiceChannel = await clientWrapper.MlkGuild.CreateVoiceChannelAsync(
            name: ServiceConsts.VoiceChannelDefaultName,
            prop =>
            {
                prop.CategoryId = categoryId;
                prop.Position = 0;

            });

        await leader.ModifyAsync(
            modify =>
            {
                modify.ChannelId = temporaryVoiceChannel.Id;
            });
    }

    public bool IsTemporaryVoiceChannel(ulong channelId)
    {
        return channelId != malenkieOptions.Value.Channels.Voice.SessionSpawners["Gaming"];
    }
}
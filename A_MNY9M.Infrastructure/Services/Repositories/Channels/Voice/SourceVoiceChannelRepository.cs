using Microsoft.EntityFrameworkCore;

using A_MNY9M.Core.Common.Enums;
using A_MNY9M.Core.Common.Result;

using A_MNY9M.Infrastructure.Database;
using A_MNY9M.Infrastructure.Models.Dtos;
using A_MNY9M.Infrastructure.Database.Entities.Channels;

namespace A_MNY9M.Infrastructure.Services.Repositories.Channels.Voice;

public sealed class SourceVoiceChannelRepository(
    AmnyamDbContext dbContext)
{
    public async Task<IReadOnlyCollection<SourceVoiceChannelDto>> GetSourceVoiceChannelsAsync(
        CancellationToken cancellationToken = default)
    {
        var channels = await dbContext
            .Set<SourceVoiceChannelDbEntity>()
            .Select(x => new SourceVoiceChannelDto(
                Id: x.Id,
                Name: x.Name,
                DiscordId: x.DiscordId,
                InitiatorDiscordId: x.InitiatorDiscordId,
                GuildCategoryId: x.GuildCategoryId,
                SetAt: x.SetAt))
            .ToListAsync(cancellationToken: cancellationToken);

        return channels;
    }

    public async Task<bool> IsSourceVoiceChannelAsync(
        ulong voiceChannelDiscordId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<SourceVoiceChannelDbEntity>()
            .AnyAsync(
                x => x.DiscordId == voiceChannelDiscordId,
                cancellationToken: cancellationToken);
    }

    public async Task<string?> RemoveSourceVoiceChannelAsync(
        ulong voiceChannelDiscordId,
        CancellationToken cancellationToken = default)
    {
        var channel = await dbContext
            .Set<SourceVoiceChannelDbEntity>()
            .Where(x => x.DiscordId == voiceChannelDiscordId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (channel is null) return null;

        var channelName = channel.Name;

        dbContext.Set<SourceVoiceChannelDbEntity>()
            .Remove(channel);

        await dbContext.SaveChangesAsync(
            cancellationToken: cancellationToken);

        return channelName;
    }

    public async Task<Result<Guid>> AddSourceVoiceChannelAsync(
        SourceVoiceChannelDbEntity sourceVoiceChannel,
        CancellationToken cancellationToken)
    {
        var exists = await dbContext
            .Set<SourceVoiceChannelDbEntity>()
            .AnyAsync(
                x => x.DiscordId == sourceVoiceChannel.DiscordId,
                cancellationToken: cancellationToken);

        if (exists)
        {
            return Result<Guid>.Fail(
                clientMessage: "Переданный канал уже является источником.",
                error: new CustomError(
                    Type: CustomErrorType.Internal, "", ""));

        }

        await dbContext
            .Set<SourceVoiceChannelDbEntity>()
            .AddAsync(
                sourceVoiceChannel, 
                cancellationToken: cancellationToken);

        await dbContext.SaveChangesAsync(
            cancellationToken: cancellationToken);

        return Result<Guid>.Success(
            sourceVoiceChannel.Id,
            $"Голосовой канал {sourceVoiceChannel.Name} назначен каналом-источником.");


    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using A_MNY9M.Core.Entities.Guild.Channel.Voice;

namespace A_MNY9M.Infrastructure.Database.EF.Configurations;

public class VoiceSessionModelConfiguration : IEntityTypeConfiguration<VoiceSession>
{
    public void Configure(EntityTypeBuilder<VoiceSession> builder)
    {
        builder.ToTable("voice_sessions").HasKey(x => x.Id);

        builder.Property(prop => prop.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(prop => prop.GuildId).HasColumnName("guild_id");
        builder.Property(prop => prop.MemberDiscordId).HasColumnName("member_discord_id");
        builder.Property(prop => prop.ChannelDiscordId).HasColumnName("channel_discord_id");
        builder.Property(prop => prop.StartingAt).HasColumnName("starting_at");
        builder.Property(prop => prop.EndingAt).HasColumnName("ending_at");
        builder.Property(prop => prop.Duration).HasColumnName("duration");
    }
}
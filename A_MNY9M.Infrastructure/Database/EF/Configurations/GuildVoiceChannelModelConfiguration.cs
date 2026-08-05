using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using A_MNY9M.Core.Entities.Guild.Channel.Voice;

namespace A_MNY9M.Infrastructure.Database.EF.Configurations;

public class GuildVoiceChannelModelConfiguration : IEntityTypeConfiguration<GuildVoiceChannel>
{
    public void Configure(EntityTypeBuilder<GuildVoiceChannel> builder)
    {
        builder.ToTable("guild_text_channels").HasKey(x => x.Id);

        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
        builder.Property(prop => prop.DiscordId).HasColumnName("discord_id");
        builder.Property(prop => prop.GuildId).HasColumnName("guild_id");
        builder.Property(prop => prop.CategoryId).HasColumnName("category_id");
        builder.Property(prop => prop.ChannelName).HasColumnName("channel_name");
        builder.Property(prop => prop.IsTemporaryChannel).HasColumnName("is_temporary_channel");
    }
}

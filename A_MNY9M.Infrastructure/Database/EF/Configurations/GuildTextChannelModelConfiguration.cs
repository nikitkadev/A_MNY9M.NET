using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using A_MNY9M.Core.Entities.Guild.Channel.Text;

namespace A_MNY9M.Infrastructure.Database.EF.Configurations;

public class GuildTextChannelModelConfiguration : IEntityTypeConfiguration<GuildTextChannel>
{
    public void Configure(EntityTypeBuilder<GuildTextChannel> builder)
    {
        builder.ToTable("guild_text_channels").HasKey(x => x.Id);

        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
        builder.Property(prop => prop.DiscordId).HasColumnName("discord_id");
        builder.Property(prop => prop.GuildId).HasColumnName("guild_id");
        builder.Property(prop => prop.CategoryId).HasColumnName("category_id");
        builder.Property(prop => prop.ChannelName).HasColumnName("channel_name");
        builder.Property(prop => prop.MessageCount).HasColumnName("message_count");
    }
}
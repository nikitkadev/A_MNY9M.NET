using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using A_MNY9M.Core.Entities.Metrics;

namespace A_MNY9M.Infrastructure.Database.EF.Configurations;

public class MemberMessageMetricModelConfiguration : IEntityTypeConfiguration<MemberMessageMetric>
{
    public void Configure(EntityTypeBuilder<MemberMessageMetric> builder)
    {
        builder.ToTable("member_message_metric").HasKey(x => x.Id);

        builder.Property(prop => prop.GuildId).HasColumnName("guild_id");
        builder.Property(prop => prop.MessageCount).HasColumnName("message_count");
        builder.Property(prop => prop.GifsSentCount).HasColumnName("gifs_sent_count");
        builder.Property(prop => prop.LastMessageAt).HasColumnName("last_message_at");
        builder.Property(prop => prop.FirstMessageAt).HasColumnName("first_message_at");
        builder.Property(prop => prop.MemberDiscordId).HasColumnName("member_discord_id");
        builder.Property(prop => prop.CommandsSentCount).HasColumnName("commands_sent_count");
        builder.Property(prop => prop.PicturesSentCount).HasColumnName("pictures_sent_count");
        builder.Property(prop => prop.StickersSentCount).HasColumnName("stickers_sent_count");
        builder.Property(prop => prop.ReactionAddedCount).HasColumnName("reaction_added_count");
    }
}
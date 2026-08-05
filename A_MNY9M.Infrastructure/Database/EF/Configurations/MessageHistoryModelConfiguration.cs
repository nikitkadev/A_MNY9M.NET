using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using A_MNY9M.Core.Entities.Guild.Channel.Text;

namespace A_MNY9M.Infrastructure.Database.EF.Configurations;

public class MessageHistoryModelConfiguration : IEntityTypeConfiguration<MessageHistory>
{
    public void Configure(EntityTypeBuilder<MessageHistory> builder)
    {
        builder.ToTable("message_history").HasKey(x => x.Id);

        builder.Property(prop => prop.Id).HasColumnName("id");
        builder.Property(prop => prop.MessageDiscordId).HasColumnName("message_discord_id");
        builder.Property(prop => prop.ChannelDiscordId).HasColumnName("channel_discord_id");
        builder.Property(prop => prop.SenderDiscordId).HasColumnName("sender_discord_id");
        builder.Property(prop => prop.SentAt).HasColumnName("sent_at");
        builder.Property(prop => prop.Content).HasColumnName("content").IsRequired(false);
        builder.Property(prop => prop.Types).HasColumnName("types");
    }
}
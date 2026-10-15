using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using A_MNY9M.Infrastructure.Database.Entities.Channels;

namespace A_MNY9M.Infrastructure.Database.Configurations.Channels;

public sealed class AutoVoiceChannelDbEntityConfiguration : IEntityTypeConfiguration<AutoVoiceChannelDbEntity>
{
    public void Configure(EntityTypeBuilder<AutoVoiceChannelDbEntity> builder)
    {
        builder.ToTable("auto_voice_channels").HasKey(entity => entity.Id);

        builder.Property(prop => prop.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(prop => prop.LeaderDiscordId).HasColumnName("leader_discord_id").IsRequired();
        builder.Property(prop => prop.DiscordId).HasColumnName("discord_id").IsRequired();
        builder.Property(prop => prop.CreatedAt).HasColumnName("created_at").IsRequired();
    }
}
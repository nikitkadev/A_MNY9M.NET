using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using A_MNY9M.Infrastructure.Database.Entities.Channels;

namespace A_MNY9M.Infrastructure.Database.Configurations.Channels;

public sealed class SourceVoiceChannelDbEntityConfiguration : IEntityTypeConfiguration<SourceVoiceChannelDbEntity>
{
    public void Configure(EntityTypeBuilder<SourceVoiceChannelDbEntity> builder)
    {
        builder.ToTable("source_voice_channels").HasKey(entity => entity.Id);

        builder.Property(prop => prop.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(prop => prop.DiscordId).HasColumnName("discord_id").IsRequired();
        builder.Property(prop => prop.Name).HasColumnName("name").IsRequired().HasMaxLength(64);
    }
}

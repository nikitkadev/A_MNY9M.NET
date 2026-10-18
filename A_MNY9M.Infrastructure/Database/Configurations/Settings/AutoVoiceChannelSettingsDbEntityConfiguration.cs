using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using A_MNY9M.Infrastructure.Database.Entities.Settings;

namespace A_MNY9M.Infrastructure.Database.Configurations.Settings;

public sealed class AutoVoiceChannelSettingsDbEntityConfiguration : IEntityTypeConfiguration<AutoVoiceChannelSettingsDbEntity>
{
    public void Configure(
        EntityTypeBuilder<AutoVoiceChannelSettingsDbEntity> builder)
    {
        builder.ToTable("auto_voice_channel_settings").HasKey(x => x.Id);

        builder.Property(prop => prop.Id).HasColumnName("id").IsRequired(true).ValueGeneratedOnAdd();
        builder.Property(prop => prop.UserDiscordId).HasColumnName("user_discord_id").IsRequired(true);
        builder.Property(prop => prop.DefaultName).HasColumnName("default_name").IsRequired(true).HasMaxLength(255);
        builder.Property(prop => prop.UserLimit).HasColumnName("user_limit").IsRequired(false);
        builder.Property(prop => prop.Region).HasColumnName("region").IsRequired(false).HasMaxLength(255);

        builder.HasIndex(x => x.UserDiscordId)
            .IsUnique();
    }
}

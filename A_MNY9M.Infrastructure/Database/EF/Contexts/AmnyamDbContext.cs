using System.Reflection;

using Microsoft.EntityFrameworkCore;

using A_MNY9M.Core.Entities.Metrics;
using A_MNY9M.Core.Entities.Settings;
using A_MNY9M.Core.Entities.Guild.Member;
using A_MNY9M.Core.Entities.Guild.Channel.Text;
using A_MNY9M.Core.Entities.Guild.Channel.Voice;

namespace A_MNY9M.Infrastructure.Database.EF.Contexts;

public class AmnyamDbContext(DbContextOptions options) : DbContext(options)
{
    DbSet<GuildMember> Members { get; set; }
    DbSet<VoiceSession> VoiceSessions { get; set; }
    DbSet<MessageHistory> MessageHistories { get; set; }
    DbSet<GuildTextChannel> TextChannels { get; set; }
    DbSet<GuildVoiceChannel> VoiceChannels { get; set; }
    DbSet<MemberMessageMetric> MessageMetrics { get; set; }
    DbSet<VoiceChannelSettings> VoiceChannelSettings { get; set; }
    DbSet<MemberVoiceSessionMetric> VoiceSessionsMetric { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using A_MNY9M.Core.Entities.Metrics;

namespace A_MNY9M.Infrastructure.Database.EF.Configurations;

public class MemberVoiceSessionMetricModelConfiguration : IEntityTypeConfiguration<MemberVoiceSessionMetric>
{
    public void Configure(EntityTypeBuilder<MemberVoiceSessionMetric> builder)
    {
        builder.ToTable("member_voice_session_metric").HasKey(x => x.Id);

    }
}

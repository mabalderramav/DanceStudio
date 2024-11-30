using DanceStudio.Domain.Subscriptions;
using DanceStudio.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DanceStudio.Infrastructure.Subcriptions.Persistence
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.AdminId);
            builder.Property(x => x.SubscriptionType).HasConversion(
                    subscriptionType => subscriptionType.Value,
                    value => SubscriptionType.FromValue(value)
                );
            builder.Property<List<Guid>>("StudioIds").HasColumnName("StudioIds").HasListOfIdsConverter();
            builder.Property("MaxStudioCount").HasColumnName("MaxStudioCount");
        }
    }
}

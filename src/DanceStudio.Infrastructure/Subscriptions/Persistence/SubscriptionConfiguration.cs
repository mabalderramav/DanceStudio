using DanceStudio.Domain.Subscriptions;
using DanceStudio.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DanceStudio.Infrastructure.Subscriptions.Persistence
{
    public class SubscriptionsConfiguration : IEntityTypeConfiguration<Subscription>
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
            builder.Property<List<Guid>>("_studioIds").HasColumnName("StudioIds").HasListOfIdsConverter();
            builder.Property("_maxStudios").HasColumnName("MaxStudios");

        }
    }
}

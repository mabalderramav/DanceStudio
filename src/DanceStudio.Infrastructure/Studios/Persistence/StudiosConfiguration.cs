using DanceStudio.Domain.Studios;
using DanceStudio.Domain.Subscriptions;
using DanceStudio.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DanceStudio.Infrastructure.Studios.Persistence
{
    public class StudiosConfiguration : IEntityTypeConfiguration<Studio>
    {
        public void Configure(EntityTypeBuilder<Studio> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property("maxRooms")
                .HasColumnName("MaxRooms");

            builder.Property<List<Guid>>("roomIds")
                .HasColumnName("RoomIds")
                .HasListOfIdsConverter();

            builder.Property<List<Guid>>("trainerIds")
                .HasColumnName("TrainerIds")
                .HasListOfIdsConverter();

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.SubscriptionId);
            builder.HasOne<Subscription>().WithMany().HasForeignKey(x => x.SubscriptionId);
        }
    }
}

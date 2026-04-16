using DanceStudio.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DanceStudio.Infrastructure.Users.Persistence
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName);
            builder.Property(u => u.LastName);
            builder.Property(u => u.Email);
            builder.Property(u => u.AdminId);
            builder.Property(u => u.ParticipantId);
            builder.Property(u => u.TrainerId);
            builder.Property("_passwordHash")
                .HasColumnName("PasswordHash");
        }
    }
}

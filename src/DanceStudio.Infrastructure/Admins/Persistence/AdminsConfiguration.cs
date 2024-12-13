using DanceStudio.Domain.Admins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DanceStudio.Infrastructure.Admins.Persistence
{
    public class AdminsConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasData(new Admin(
                userId: Guid.Parse("6c526115-69db-4fa4-aa69-0ad3bb9f0de7"),
                id: Guid.Parse("9b874476-d3ed-430d-8dfb-934582487dc1")));
        }
    }
}

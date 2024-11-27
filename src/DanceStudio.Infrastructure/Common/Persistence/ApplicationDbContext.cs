using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace DanceStudio.Infrastructure.Common.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // Fuent API
        public DbSet<Subscription>?  Subscriptions { get; set; }

        public async Task CommitChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}

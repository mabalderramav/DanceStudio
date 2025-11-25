using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Admins;
using DanceStudio.Domain.Studios;
using DanceStudio.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DanceStudio.Infrastructure.Common.Persistence
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
    {
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Studio> Studios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations from all assemblies.
            // This will apply all configurations from the current assembly and all referenced assemblies.
            // With this approach, you don't need to manually add each configuration to the model builder.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task CommitChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}

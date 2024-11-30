using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DanceStudio.Infrastructure.Common.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

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

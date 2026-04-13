using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Admins;
using DanceStudio.Domain.Studios;
using DanceStudio.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DanceStudio.Domain.Common;
using DanceStudio.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DanceStudio.Infrastructure.Common.Persistence
{
    public class ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : DbContext(options), IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
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
            //get hold of all the domain events
            var domainEvents = ChangeTracker.Entries<Entity>()
                .Select(x => x.Entity.PopDomainEvents())
                .SelectMany(x => x)
                .ToList();
            //store them in the http context for later
            AddDomainEventsToOfflineProcessingQueue(domainEvents);
            
            await base.SaveChangesAsync();
        }
        
        private void AddDomainEventsToOfflineProcessingQueue(IEnumerable<IDomainEvent> domainEvents)
        {
            //fetch queue from http context or create a new query if it doesn't exist
            var domainEventsQueue = httpContextAccessor.HttpContext!.Items
                .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents ?
                existingDomainEvents :
                new Queue<IDomainEvent>();

            //add the domain events to the end of queue
            foreach (var item in domainEvents)
            {
                domainEventsQueue.Enqueue(item);
            }
            
            //  so it can be retrieved by the offline processing worker
            httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
        }
    }
}

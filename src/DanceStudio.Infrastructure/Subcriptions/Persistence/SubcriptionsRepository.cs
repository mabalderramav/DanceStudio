using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Subscriptions;
using DanceStudio.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DanceStudio.Infrastructure.Subcriptions.Persistence
{
    public class SubcriptionsRepository : ISubscriptionsRepository
    {
        private readonly ApplicationDbContext context;

        public SubcriptionsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await context.Set<Subscription>().AddAsync(subscription);
        }

        public async Task<Subscription?> GetByIdAsync(Guid subcriptionId)
        {
            return await context.Set<Subscription>().FindAsync(subcriptionId);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Subscriptions
                .AsNoTracking()
                .AnyAsync(subscription => subscription.Id == id);
        }
        public async Task<Subscription?> GetByAdminIdAsync(Guid adminId)
        {
            return await context.Subscriptions
                .AsNoTracking()
                .FirstOrDefaultAsync(subscription => subscription.AdminId == adminId);
        }
        public async Task<List<Subscription>> ListAsync()
        {
            return await context.Subscriptions.ToListAsync();
        }
        public Task RemoveSubscriptionAsync(Subscription subscription)
        {
            context.Remove(subscription);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(Subscription subscription)
        {
            context.Update(subscription);
            return Task.CompletedTask;
        }
    }
}

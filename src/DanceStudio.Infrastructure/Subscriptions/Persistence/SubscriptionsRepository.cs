using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Subscriptions;
using DanceStudio.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DanceStudio.Infrastructure.Subscriptions.Persistence
{
    public class SubscriptionsRepository(ApplicationDbContext context) : ISubscriptionsRepository
    {
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

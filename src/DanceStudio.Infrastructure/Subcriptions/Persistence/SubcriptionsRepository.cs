using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Subscriptions;
using DanceStudio.Infrastructure.Common.Persistence;

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
            await context.Subscriptions.AddAsync(subscription);
        }

        public async Task<Subscription?> GetByIdAsync(Guid subcriptionId)
        {
            return await context.Subscriptions.FindAsync(subcriptionId);
        }
    }
}

using DanceStudio.Domain.Subscriptions;

namespace DanceStudio.Application.Common.Interfaces
{
    public interface ISubscriptionsRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);
        Task<Subscription> GetByIdAsync(Guid subscriptionId);
    }
}

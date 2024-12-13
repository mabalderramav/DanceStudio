using DanceStudio.Domain.Subscriptions;

namespace DanceStudio.Application.Common.Interfaces
{
    public interface ISubscriptionsRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);
        Task<Subscription> GetByIdAsync(Guid subscriptionId);
        Task<bool> ExistsAsync(Guid id);
        Task<Subscription?> GetByAdminIdAsync(Guid adminId);
        Task<List<Subscription>> ListAsync();
        Task RemoveSubscriptionAsync(Subscription subscription);
        Task UpdateAsync(Subscription subscription);
    }
}

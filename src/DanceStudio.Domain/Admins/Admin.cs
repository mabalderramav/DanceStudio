using DanceStudio.Domain.Admins.Events;
using DanceStudio.Domain.Common;
using DanceStudio.Domain.Subscriptions;
using Throw;

namespace DanceStudio.Domain.Admins
{
    public class Admin(Guid userId, Guid? subscriptionId = null, Guid? id = null) : Entity
    {
        public Guid UserId { get; } = userId;
        public Guid? SubscriptionId { get; private set; } = subscriptionId;

        public void SetSubscription(Subscription subscription)
        {
            SubscriptionId.HasValue.Throw().IfTrue();
            SubscriptionId = subscription.Id;
        }

        public void DeleteSubscription(Guid subscriptionId)
        {
            SubscriptionId.ThrowIfNull().IfNotEquals(subscriptionId);
            SubscriptionId = null;
            DomainEvents.Add(new SubscriptionDeletedEvent(subscriptionId));
        }
    }
}

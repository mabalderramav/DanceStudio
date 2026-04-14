using DanceStudio.Domain.Admins.Events;
using DanceStudio.Domain.Common;
using DanceStudio.Domain.Subscriptions;
using Throw;

namespace DanceStudio.Domain.Admins
{
    public class Admin : Entity
    {
        public Guid UserId { get; private set; }
        public Guid? SubscriptionId { get; private set; }

        private Admin() { } // For EF Core

        public Admin(Guid userId, Guid? subscriptionId = null, Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            UserId = userId;
            SubscriptionId = subscriptionId;
        }

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

using DanceStudio.Domain.Common.Interfaces;

namespace DanceStudio.Domain.Admins.Events
{
    public record SubscriptionDeletedEvent(Guid SubscriptionId) : IDomainEvent;
}

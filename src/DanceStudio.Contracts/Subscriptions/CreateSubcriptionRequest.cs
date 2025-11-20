namespace DanceStudio.Contracts.Subscriptions
{
    public record CreateSubscriptionRequest(
        SubscriptionType SubscriptionType,
        Guid AdminId);
}

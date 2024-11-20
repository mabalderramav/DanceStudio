namespace DanceStudio.Contracts.Subcriptions
{
    public record CreateSubscriptionRequest(
        SubscriptionType SubscriptionType,
        Guid AdminId);
}

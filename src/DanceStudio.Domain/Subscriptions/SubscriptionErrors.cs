using ErrorOr;

namespace DanceStudio.Domain.Subscriptions
{
    public static class SubscriptionErrors
    {
        public static Error CannotHaveMoreStudiosThanTheSubscriptionAllows =
            Error.Validation(
                "Subscription.CannotHaveMoreStudiosThanTheSubscriptionAllows",
                "A subscription cannot have more studios than it allows."
                );
    }
}

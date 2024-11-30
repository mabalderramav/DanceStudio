using ErrorOr;

namespace DanceStudio.Domain.Studios
{
    public static class StudioErrors
    {
        public static Error CannotHaveMoreRoomsThanTheSubscriptionAllows =
            Error.Validation(
                "Studio.CannotHaveMoreRoomsThanTheSubscriptionAllows",
                "A studio cannot have more rooms than the subscription allows."
                );
    }
}

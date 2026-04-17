using DanceStudio.Domain.Subscriptions;

namespace DanceStudio.TestCommon.TestConstants
{
    public static class Constants
    {
        public static class Admin
        {
            public static Guid Id = new("876e7269-5e3c-4834-9bbf-b2d58cad5ea7");
        }

        public static class Studio
        {
            public static readonly Guid Id = Guid.NewGuid();
            public const string Name = "StudioX";
        }

        public static class Subscriptions
        {
            public static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
            public static readonly Guid Id = Guid.NewGuid();            
            public const int MaxRoomsFreeTier = 1;
            public const int MaxStudiosFreeTier = 1;
        }
    }
}

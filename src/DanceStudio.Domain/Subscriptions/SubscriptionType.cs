using Ardalis.SmartEnum;

namespace DanceStudio.Domain.Subscriptions
{
    public class SubscriptionType : SmartEnum<SubscriptionType>
    {
        public SubscriptionType(string name, int value) : base(name, value)
        {
        }

        public static readonly SubscriptionType Free = new SubscriptionType(nameof(Free), 0);
        public static readonly SubscriptionType Starter = new SubscriptionType(nameof(Starter), 1);
        public static readonly SubscriptionType Pro = new SubscriptionType(nameof(Pro), 2);
    }
}

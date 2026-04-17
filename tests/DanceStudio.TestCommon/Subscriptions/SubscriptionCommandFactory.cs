using DanceStudio.Application.Subscriptions.Commands.CreateSubscription;
using DanceStudio.Domain.Subscriptions;
using DanceStudio.TestCommon.TestConstants;

namespace DanceStudio.TestCommon.Subscriptions
{
    public static class SubscriptionCommandFactory
    {
        public static CreateSubscriptionCommand CreateCreateSubscriptionCommand(
            SubscriptionType? subscriptionType = null,
            Guid? adminId = null)
        {
            return new CreateSubscriptionCommand(
                SubscriptionType: subscriptionType ?? Constants.Subscriptions.DefaultSubscriptionType,
                AdminId: adminId ?? Constants.Admin.Id);
        }
    }
}

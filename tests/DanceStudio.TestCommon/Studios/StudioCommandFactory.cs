using DanceStudio.Application.Studios.Commands.CreateStudio;
using DanceStudio.TestCommon.TestConstants;

namespace DanceStudio.TestCommon.Studios
{
    public static class StudioCommandFactory
    {
        public static CreateStudioCommand CreateCreateStudioCommand(
            string name = Constants.Studio.Name,
            Guid? subscriptionId = null)
        {
            return new CreateStudioCommand(
                Name: name,
                SubscriptionId: subscriptionId ?? Constants.Subscriptions.Id);
        }
    }
}

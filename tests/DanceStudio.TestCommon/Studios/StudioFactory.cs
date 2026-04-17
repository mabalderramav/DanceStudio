using DanceStudio.Domain.Studios;
using DanceStudio.TestCommon.TestConstants;

namespace DanceStudio.TestCommon.Studios
{
    public static class StudioFactory
    {
        public static Studio CreateStudio(
            string name = Constants.Studio.Name,
            int maxRooms = Constants.Subscriptions.MaxRoomsFreeTier,
            Guid? id = null)
        {
            return new Studio(
                name,
                maxRooms,
                subscriptionId: Constants.Subscriptions.Id,
                id: id ?? Constants.Studio.Id);
        }
    }
}

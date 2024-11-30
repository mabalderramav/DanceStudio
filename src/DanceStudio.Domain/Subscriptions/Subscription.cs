using DanceStudio.Domain.Studios;
using ErrorOr;
using Throw;

namespace DanceStudio.Domain.Subscriptions
{
    public class Subscription
    {
        public Guid Id { get; private set; }
        public SubscriptionType SubscriptionType { get; private set; } = default!;
        public Guid AdminId { get; }
        private readonly List<Guid> StudioIds = new();
        private readonly int MaxStudioCount;



        private Subscription()
        {
        }

        public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
        {
            SubscriptionType = subscriptionType;
            AdminId = adminId;
            Id = id ?? Guid.NewGuid();
        }

        #region bussiness logic
        public ErrorOr<Success> AddStudio(Studio studio)
        {
            StudioIds.Throw().IfContains(studio.Id);

            if (StudioIds.Count >= MaxStudioCount)
            {
                return SubscriptionErrors.CannotHaveMoreStudiosThanTheSubscriptionAllows;
            }

            StudioIds.Add(studio.Id);
            return new Success();
        }

        public int GetMaxStudio() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 2,
            nameof(SubscriptionType.Pro) => 3,
            _ => throw new InvalidOperationException()
        };

        public int GetMaxRooms() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 3,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException()
        };

        public bool HasStudio(Guid studioId) => StudioIds.Contains(studioId);

        public void RemoveStudio(Guid studioId)
        {
            StudioIds.Throw().IfNotContains(studioId);
            StudioIds.Remove(studioId);
        }
        #endregion
    }
}

using DanceStudio.Domain.Studios;
using ErrorOr;
using Throw;

namespace DanceStudio.Domain.Subscriptions
{
    public class Subscription
    {
        public Guid Id { get; private set; }
        public SubscriptionType SubscriptionType { get; private set; }
        public Guid AdminId { get; }
        private readonly List<Guid> _studioIds = [];
        private readonly int _maxStudios;

        public Subscription()
        {
            SubscriptionType = null!;
        }

        public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
        {
            SubscriptionType = subscriptionType;
            AdminId = adminId;
            Id = id ?? Guid.NewGuid();
            _maxStudios = GetMaxStudios();
        }

        #region businessLogic
        public ErrorOr<Success> AddStudio(Studio studio)
        {
            _studioIds.Throw().IfContains(studio.Id);

            if (_studioIds.Count >= _maxStudios)
            {
                return SubscriptionErrors.CannotHaveMoreStudiosThanTheSubscriptionAllows;
            }
            _studioIds.Add(studio.Id);
            return Result.Success;
        }

        public int GetMaxStudios() => SubscriptionType.Name switch
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

        public bool HasStudio(Guid studioId)
        {
            return _studioIds.Contains(studioId);
        }

        public void RemoveStudio(Guid studioId)
        {
            _studioIds.Throw().IfNotContains(studioId);
            _studioIds.Remove(studioId);
        }

        #endregion

    }
}

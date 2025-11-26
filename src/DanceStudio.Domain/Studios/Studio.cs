using DanceStudio.Domain.Rooms;
using ErrorOr;
using Throw;

namespace DanceStudio.Domain.Studios
{
    public class Studio(string name, int maxRooms, Guid subscriptionId, Guid? id = null)
    {
        public Guid Id { get; } = id ?? Guid.NewGuid();
        private readonly List<Guid> _roomIds = [];
        private readonly List<Guid> _trainerIds = [];

        public string Name { get; init; } = name;
        public Guid SubscriptionId { get; init; } = subscriptionId;

        public ErrorOr<Success> AddRoom(Room room)
        {
            _roomIds.Throw().IfContains(room.Id);

            if (_roomIds.Count >= maxRooms)
            {
                return StudioErrors.CannotHaveMoreRoomsThanTheSubscriptionAllows;
            }

            _roomIds.Add(room.Id);

            return Result.Success;
        }

        public bool HasRoom(Guid roomId)
        {
            return _roomIds.Contains(roomId);
        }

        public ErrorOr<Success> AddTrainer(Guid trainerId)
        {
            if (_trainerIds.Contains(trainerId))
            {
                return Error.Conflict(description: "Trainer already added to studio");
            }

            _trainerIds.Add(trainerId);
            return Result.Success;
        }

        public bool HasTrainer(Guid trainerId)
        {
            return _trainerIds.Contains(trainerId);
        }

        public void RemoveRoom(Guid roomId)
        {
            _roomIds.Remove(roomId);
        }
    }
}

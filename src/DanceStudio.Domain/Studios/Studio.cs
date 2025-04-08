using DanceStudio.Domain.Rooms;
using ErrorOr;
using Throw;

namespace DanceStudio.Domain.Studios
{
    public class Studio
    {
        private readonly int maxRooms;

        public Guid Id { get; }
        private readonly List<Guid> _roomIds = new();
        private readonly List<Guid> _trainerIds = new();

        public string Name { get; init; } = null!;
        public Guid SubscriptionId { get; init; }

        public Studio(string name, int maxRooms, Guid subscriptionId, Guid? id = null)
        {
            Name = name;
            this.maxRooms = maxRooms;
            SubscriptionId = subscriptionId;
            Id = id ?? Guid.NewGuid();
        }

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

        private Studio() { }
    }
}

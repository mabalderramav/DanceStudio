using DanceStudio.Domain.Rooms;
using ErrorOr;
using Throw;

namespace DanceStudio.Domain.Studios
{
    public class Studio
    {
        private readonly int maxRooms;

        public Guid Id { get; }
        private readonly List<Guid> roomIds = new();
        private readonly List<Guid> trainerIds = new();

        public string Name { get; init; } = null!;
        public Guid SubscriptionId { get; init; }

        public Studio(
            string name,
            int maxRooms,
            Guid subscriptionId,
            Guid? id = null)
        {
            Name = name;
            this.maxRooms = maxRooms;
            SubscriptionId = subscriptionId;
            Id = id ?? Guid.NewGuid();
        }

        public ErrorOr<Success> AddRoom(Room room)
        {
            Console.WriteLine("roomIds");
            roomIds.Throw().IfContains(room.Id);

            if (roomIds.Count >= maxRooms)
            {
                return StudioErrors.CannotHaveMoreRoomsThanTheSubscriptionAllows;
            }

            roomIds.Add(room.Id);

            return Result.Success;
        }

        public bool HasRoom(Guid roomId)
        {
            return roomIds.Contains(roomId);
        }

        public ErrorOr<Success> AddTrainer(Guid trainerId)
        {
            if (trainerIds.Contains(trainerId))
            {
                return Error.Conflict(description: "Trainer already added to studio");
            }

            trainerIds.Add(trainerId);
            return Result.Success;
        }

        public bool HasTrainer(Guid trainerId)
        {
            return trainerIds.Contains(trainerId);
        }

        public void RemoveRoom(Guid roomId)
        {
            roomIds.Remove(roomId);
        }

        private Studio() { }
    }
}

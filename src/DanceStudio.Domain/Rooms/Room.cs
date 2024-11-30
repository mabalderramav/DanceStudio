namespace DanceStudio.Domain.Rooms
{
    public class Room
    {
        public Guid Id { get; }
        public string Name { get; } = default!;

        public Guid StudioId { get; }

        public Room(string name, Guid studioId, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
            StudioId = studioId;
        }
    }
}

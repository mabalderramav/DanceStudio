using DanceStudio.Domain.Common.Interfaces;

namespace DanceStudio.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; init; }
        protected readonly List<IDomainEvent> DomainEvents = [];

        protected Entity() { }
        protected Entity(Guid id) => Id = id;

        public List<IDomainEvent> PopDomainEvents()
        {
            var tmp = DomainEvents.ToList();
            DomainEvents.Clear();
            return tmp;
        }
    }
}

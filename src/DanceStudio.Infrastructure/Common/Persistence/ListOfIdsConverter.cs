using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DanceStudio.Infrastructure.Common.Persistence
{
    public class ListOfIdsConverter : ValueConverter<List<Guid>, string>
    {
        public ListOfIdsConverter(ConverterMappingHints? mappingHints = null)
            : base(
                  x => string.Join(',', x),
                  x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList(),
                  mappingHints)
        {
        }
    }

    public class ListOfIdsComparer : ValueComparer<List<Guid>>
    {
        public ListOfIdsComparer() : base(
            (t1, t2) => t1!.SequenceEqual(t2!),
            t => t.Select(x => x!.GetHashCode()).Aggregate((x, y) => x ^ y),
            t => t
            )
        {
        }
    }
}

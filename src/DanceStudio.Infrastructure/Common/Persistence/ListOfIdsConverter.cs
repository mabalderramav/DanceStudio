using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DanceStudio.Infrastructure.Common.Persistence
{
    public class ListOfIdsConverter(ConverterMappingHints? mappingHints = null) : ValueConverter<List<Guid>, string>(
        x => string.Join(',', x),
        x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList(),
        mappingHints);

    public class ListOfIdsComparer() : ValueComparer<List<Guid>>((t1, t2) => t1!.SequenceEqual(t2!),
        t => t.Select(x => x.GetHashCode()).Aggregate((x, y) => x ^ y),
        t => t);
}

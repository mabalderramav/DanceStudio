using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace DanceStudio.Infrastructure.Common.Persistence
{
    public class ValueJsonConverter<T> : ValueConverter<T, string>
    {
        public ValueJsonConverter(ConverterMappingHints? mappingHints = null)
            : base(
                  x => JsonSerializer.Serialize(x, JsonSerializerOptions.Default),
                  x => JsonSerializer.Deserialize<T>(x, JsonSerializerOptions.Default)!,
                  mappingHints
                  )
        {
        }
    }

    public class ValueJsonComparer<T> : ValueComparer<T>
    {
        public ValueJsonComparer() : base(
          (l, r) => JsonSerializer.Serialize(l, JsonSerializerOptions.Default) == JsonSerializer.Serialize(r, JsonSerializerOptions.Default),
          v => v == null ? 0 : JsonSerializer.Serialize(v, JsonSerializerOptions.Default).GetHashCode(),
          v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, JsonSerializerOptions.Default), JsonSerializerOptions.Default)!)
        {
        }
    }
}

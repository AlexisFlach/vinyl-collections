using System;

namespace VinylCollection.Entities
{
    public record Vinyl
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Artist { get; init; }
    }
}
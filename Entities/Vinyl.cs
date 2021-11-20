using System;

namespace VinylCollection.Entities
{
    public record Vinyl
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
    }
}
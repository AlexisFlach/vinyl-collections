using System;

namespace VinylCollection.Dtos
{
       public record VinylDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Artist { get; init; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace VinylCollection.Dtos
{
       public record UpdateVinylDto
    {   
        [Required]
        public string Title { get; init; }
        
        [Required]
        public string Artist { get; init; }
    }
}
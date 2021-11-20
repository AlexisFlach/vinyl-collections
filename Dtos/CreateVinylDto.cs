using System;
using System.ComponentModel.DataAnnotations;

namespace VinylCollection.Dtos
{
       public record CreateVinylDto
    {   
        [Required]
        public string Title { get; init; }
        
        [Required]
        public string Artist { get; init; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylCollection.Entities;
using VinylCollection.Persistence;

namespace VinylCollection.Seed
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Vinyls.Any()) return;
            
            var activities = new List<Vinyl>
            {
                new Vinyl
                {
                    Title = "Bringing it all back home",
                    Artist = "Bob Dylan ",
                },
                new Vinyl
                {
                    Title = "Bringing it all back",
                    Artist = "JJ Cale",
                },
                new Vinyl
                {
                    Title = "Magic",
                    Artist = "Bruce Springsteen",
                },
                
            };

            await context.Vinyls.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}
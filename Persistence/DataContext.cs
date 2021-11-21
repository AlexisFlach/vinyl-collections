using Microsoft.EntityFrameworkCore;
using VinylCollection.Entities;

namespace VinylCollection.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Vinyl> Vinyls { get; set; }
    }
}
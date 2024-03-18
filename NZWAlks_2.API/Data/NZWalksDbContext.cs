using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NZWAlks_2.API.Models.Domain;

namespace NZWAlks_2.API.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {    
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }
    }
}

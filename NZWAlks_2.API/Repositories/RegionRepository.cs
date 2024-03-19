using Microsoft.EntityFrameworkCore;
using NZWAlks_2.API.Data;
using NZWAlks_2.API.Models.Domain;
using NZWAlks_2.API.Repository;

namespace NZWAlks_2.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public IEnumerable<Region> GetAll()
        {
            return nZWalksDbContext.Regions.ToList();
        }

        public async Task<IEnumerable<Region>> GetAll_async()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }
    }
}

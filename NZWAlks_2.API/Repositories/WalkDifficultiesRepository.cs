using Microsoft.EntityFrameworkCore;
using NZWAlks_2.API.Data;
using NZWAlks_2.API.Models.Domain;

namespace NZWAlks_2.API.Repositories
{
    public class WalkDifficultiesRepository : IWalkDifficultiesRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultiesRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.WalkDifficulties.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWalkDifficulties = await nZWalksDbContext.WalkDifficulties.FindAsync(id);
            if (existingWalkDifficulties != null)
            {
                nZWalksDbContext.WalkDifficulties.Remove(existingWalkDifficulties);
                await nZWalksDbContext.SaveChangesAsync();
                return existingWalkDifficulties;
            }
            return null;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetByIdAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }
            existingWalkDifficulty.Code = walkDifficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}

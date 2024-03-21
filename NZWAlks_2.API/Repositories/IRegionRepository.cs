using NZWAlks_2.API.Models.Domain;

namespace NZWAlks_2.API.Repository
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetAll();
        Task<IEnumerable<Region>> GetAll_async();
        Task<Region> GetAsync(Guid id);
        Task<Region> AddAsync(Region region);
        Task<Region> DeleteAsync(Guid id);
        Task<Region> UpdateAsync(Guid id,Region region);
    }
}

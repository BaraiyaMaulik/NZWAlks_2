using NZWAlks_2.API.Models.Domain;

namespace NZWAlks_2.API.Repository
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetAll();
        Task<IEnumerable<Region>> GetAll_async();
    }
}

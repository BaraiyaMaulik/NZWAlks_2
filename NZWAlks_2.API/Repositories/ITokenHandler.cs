using NZWAlks_2.API.Models.Domain;

namespace NZWAlks_2.API.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}

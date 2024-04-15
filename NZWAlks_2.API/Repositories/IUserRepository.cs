using NZWAlks_2.API.Models.Domain;

namespace NZWAlks_2.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}

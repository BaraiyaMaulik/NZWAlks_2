﻿using Microsoft.EntityFrameworkCore;
using NZWAlks_2.API.Data;
using NZWAlks_2.API.Models.Domain;

namespace NZWAlks_2.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public UserRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await nZWalksDbContext.Users
                .FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var userRoles = await nZWalksDbContext.Users_Roles.Where(x => x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await nZWalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = null; return user;
        }
    }
}

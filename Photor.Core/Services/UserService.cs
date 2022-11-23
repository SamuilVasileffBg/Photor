using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Core.Models.User;
using Photor.Infrastructure.Data;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task EditAccountAsync(UserViewModel model)
        {
            var user = await GetUserByIdAsync(model.Id);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(model), "Cannot find user with such id.");
            }

            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Description = model.Description;

            await context.SaveChangesAsync();
        }

        public ApplicationUser? GetUserById(string userId)
        {
            return context
                .Users
                .ToList()
                .FirstOrDefault(u => u.Id == userId);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<UserViewModel>?> SearchUsersAsync(string? searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return null;
            }

            var data = await context
                .Users
                .Where(u => u.UserName.ToUpper().Contains(searchValue.ToUpper()))
                .Select(u => new UserViewModel
                {
                    UserName = u.UserName,
                    Id = u.Id
                })
                .ToListAsync();

            return data;
        }
    }
}

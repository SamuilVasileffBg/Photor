using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Core.Models;
using Photor.Infrastructure.Data;
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

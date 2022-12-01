using Photor.Core.Models.User;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Contracts
{
    public interface IUserService
    {
        public Task<int> SearchUsersCountAsync(string searchValue);
        public Task<IEnumerable<UserViewModel>?> SearchUsersAsync(string name);

        public Task<IEnumerable<UserViewModel>?> SearchUsersAsync(string name, int page);

        public Task<ApplicationUser> GetUserByIdAsync(string userId);

        public Task EditAccountAsync(UserViewModel model);

        public ApplicationUser GetUserById(string userId);
    }
}

using Photor.Core.Models;
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
        public Task<IEnumerable<UserViewModel>?> SearchUsersAsync(string name);

        public Task<ApplicationUser> GetUserByIdAsync(string userId);

        public ApplicationUser GetUserById(string userId);
    }
}

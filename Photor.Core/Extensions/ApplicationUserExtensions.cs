using Photor.Core.Models;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Parsers
{
    public static class ApplicationUserExtensions
    {
        public static UserViewModel ParseToViewModel(this ApplicationUser applicationUser)
        {
            return new UserViewModel()
            {
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Description = applicationUser.Description,
            };
        }
    }
}

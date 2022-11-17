using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models
{
    public class UserSearchViewModel
    {
        public string? SearchValue { get; set; }

        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}

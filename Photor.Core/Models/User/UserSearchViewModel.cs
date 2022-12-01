using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models.User
{
    public class UserSearchViewModel
    {
        public string? SearchValue { get; set; }

        public int? Page { get; set; }

        public int AllMatchesCount { get; set; }

        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}

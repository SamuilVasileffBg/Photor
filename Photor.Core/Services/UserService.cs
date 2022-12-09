using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Core.Models.User;
using Photor.Core.Parsers;
using Photor.Infrastructure.Data;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;

namespace Photor.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        private readonly IGoogleDriveService googleDriveService;

        public UserService(IGoogleDriveService googleDriveService, IRepository repository)
        {
            this.googleDriveService = googleDriveService;
            this.repository = repository;
        }

        public async Task EditAccountAsync(UserViewModel model)
        {
            var user = await GetUserByIdAsync(model.Id);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(model), "Cannot find user with such id.");
            }


            if (model.Image != null)
            {
                string? imageUrl = null;

                imageUrl = await googleDriveService.UploadImageAsync(model.Image);

                if (String.IsNullOrEmpty(imageUrl))
                {
                    throw new Exception("Upload unsuccessful");
                }

                user.ImageUrl = imageUrl;
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Description = model.Description;

            await repository.SaveChangesAsync();
        }

        public ApplicationUser? GetUserById(string userId)
        {
            return repository
                .All<ApplicationUser>()
                .ToList()
                .FirstOrDefault(u => u.Id == userId);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await repository
                .All<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<UserViewModel>?> SearchUsersAsync(string? searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return null;
            }

            var data = await repository
                .All<ApplicationUser>()
                .Where(u => u.UserName.ToUpper().Contains(searchValue.ToUpper()))
                .Select(u => new UserViewModel
                {
                    UserName = u.UserName,
                    Id = u.Id
                })
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<UserViewModel>?> SearchUsersAsync(string searchValue, int page)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return null;
            }

            var data = await repository
                .All<ApplicationUser>()
                .Where(u => u.UserName.ToUpper().Contains(searchValue.ToUpper()))
                .Skip((page - 1) * 5)
                .Take(5)
                .Select(u => u.ParseToViewModel())
                .ToListAsync();

            return data;
        }

        public async Task<int> SearchUsersCountAsync(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return 0;
            }

            var count = await repository
                .All<ApplicationUser>()
                .Where(u => u.UserName.ToUpper().Contains(searchValue.ToUpper()))
                .CountAsync();

            return count;
        }
    }
}

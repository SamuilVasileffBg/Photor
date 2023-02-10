using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(CreateUsers());
        }

        private List<ApplicationUser> CreateUsers()
        {
            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var admin = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "SamiAdmin",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
            };

            var moderator = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d839508M",
                UserName = "SamiModerator",
                FirstName = "Samuil",
                LastName = "Moderatorov",
                NormalizedUserName = "SamiModerator".ToUpper(),
                Email = "samimoderator@mail.com",
                NormalizedEmail = "samimoderator@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
            };

            var user = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d839508U",
                UserName = "SamiUser",
                FirstName = "Samuil",
                LastName = "Userov",
                NormalizedUserName = "SamiUser".ToUpper(),
                Email = "samiuser@mail.com",
                NormalizedEmail = "samiuser@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
            };

            admin.PasswordHash =
                 hasher.HashPassword(admin, "SamiAdmin!1");
            moderator.PasswordHash =
                 hasher.HashPassword(moderator, "SamiModerator!1");
            user.PasswordHash =
                 hasher.HashPassword(admin, "SamiUser!1");

            users.Add(admin);
            users.Add(moderator);
            users.Add(user);

            return users;
        }

    }
}

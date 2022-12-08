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

            var user = new ApplicationUser()
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

            user.PasswordHash =
                 hasher.HashPassword(user, "SamiAdmin!1");

            users.Add(user);

            return users;
        }

    }
}

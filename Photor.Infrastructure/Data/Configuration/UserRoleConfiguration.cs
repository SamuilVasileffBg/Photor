using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Infrastructure.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>
            {
                UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
            });
            builder.HasData(new IdentityUserRole<string>
            {
                UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                RoleId = "683cd19b-7a34-4cc8-b3cc-64547e4f125f",
            });
            builder.HasData(new IdentityUserRole<string>
            {
                UserId = "dea12856-c198-4129-b3f3-b893d839508M",
                RoleId = "683cd19b-7a34-4cc8-b3cc-64547e4f125f",
            });
            builder.HasData(new IdentityUserRole<string>
            {
                UserId = "dea12856-c198-4129-b3f3-b893d839508M",
                RoleId = "145bdb34-18fa-4644-b002-6e4a02147b77",
            });
            builder.HasData(new IdentityUserRole<string>
            {
                UserId = "dea12856-c198-4129-b3f3-b893d839508U",
                RoleId = "683cd19b-7a34-4cc8-b3cc-64547e4f125f",
            });
        }
    }
}

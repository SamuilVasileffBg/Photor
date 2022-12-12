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
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole {
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR".ToUpper(),
            });
            builder.HasData(new IdentityRole
            {
                Id = "683cd19b-7a34-4cc8-b3cc-64547e4f125f",
                Name = "User",
                NormalizedName = "USER".ToUpper(),
            });
        }
    }
}

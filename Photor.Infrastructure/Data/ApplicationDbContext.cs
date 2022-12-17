using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Photor.Infrastructure.Data.Configuration;
using Photor.Infrastructure.Data.Models;

namespace Photor.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserFriend> UsersFriends { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<UserLikedPost> UsersLikedPosts { get; set; }

        public DbSet<UserPostComment> UsersPostComments { get; set; }

        public DbSet<UserPostReport> UsersPostReports { get; set; }

        public DbSet<UserSavedPost> UsersSavedPosts { get; set; }

        public DbSet<FriendInvitation> FriendInvitations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<ApplicationUser>()
                .HasMany(u => u.UserLikedPosts)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Post>()
                .HasMany(u => u.PostLikes)
                .WithOne(f => f.Post)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<ApplicationUser>()
                .HasMany(u => u.UserPostComments)
                .WithOne(upc => upc.User)
                .HasForeignKey(upc => upc.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<ApplicationUser>()
                .HasMany(u => u.UserPostReports)
                .WithOne(upr => upr.User)
                .HasForeignKey(upr => upr.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<ApplicationUser>()
                .HasMany(u => u.UserSavedPosts)
                .WithOne(usp => usp.User)
                .HasForeignKey(usp => usp.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
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

        //public DbSet<FriendInvitation> FriendsInvitations { get; set; }

        //public DbSet<Image> Images { get; set; }

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

            //builder.Entity<FriendInvitation>()
            //    .HasKey(fi => new { fi.SenderUserId, fi.ReceiverWebAccId });

            //builder.Entity<UserFriend>()
            //    .HasKey(f => new { f.UserId, f.FriendId });

            //builder.Entity<UserLikedPost>()
            //    .HasKey(upl => new { upl.UserId, upl.PostId });

            //builder.Entity<UserSavedPost>()
            //    .HasKey(usp => new { usp.UserId, usp.PostId });

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
            //builder
            //    .Entity<ApplicationUser>()
            //    .HasMany(u => u.UserFriends)
            //    .WithOne(uf => uf.User)
            //    .HasForeignKey(uf => uf.UserId)
            //    .OnDelete(DeleteBehavior.NoAction);
            //
            //builder
            //    .Entity<ApplicationUser>()
            //    .HasMany(u => u.UserFriends)
            //    .WithOne(uf => uf.Friend)
            //    .HasForeignKey(uf => uf.FriendId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder
            //    .Entity<ApplicationUser>()
            //    .HasMany(u => u.FriendInvitations)
            //    .WithOne(fi => fi.SenderUser)
            //    .HasForeignKey(fi => fi.SenderUserId)
            //    .OnDelete(DeleteBehavior.NoAction);
            //
            //builder
            //    .Entity<ApplicationUser>()
            //    .HasMany(u => u.FriendInvitations)
            //    .WithOne(fi => fi.ReceiverUser)
            //    .HasForeignKey(fi => fi.ReceiverWebAccId)
            //    .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
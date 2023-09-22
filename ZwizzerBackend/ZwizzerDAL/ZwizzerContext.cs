using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwizzerDAL
{
    public class ZwizzerContext : DbContext
    {
        public ZwizzerContext()
        {
        }

        public ZwizzerContext(DbContextOptions<ZwizzerContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Zweet> Zweets { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rezweet> Rezweets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>().HasMany(u => u.Zweets).WithOne(z => z.User).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(u => u.Likes).WithOne(z => z.User).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(u => u.Rezweets).WithOne(z => z.User).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(u => u.Comments).WithOne(z => z.User).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Zweet>().HasMany(z => z.Comments).WithOne(c => c.Zweet).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Zweet>().HasMany(z => z.Rezweets).WithOne(c => c.Zweet).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Zweet>().HasMany(z => z.Likes).WithOne(c => c.Zweet).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Zweet>().HasOne(z => z.User).WithMany(u => u.Zweets).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>().HasOne(c => c.Zweet).WithMany(z => z.Comments).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(z => z.Comments).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>().HasMany(c => c.Likes).WithOne(c => c.Comment).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>().Property(l => l.CommentId).IsRequired(false);
            modelBuilder.Entity<Like>().Property(l => l.ZweetId).IsRequired(false);
            modelBuilder.Entity<Like>().HasOne(c => c.User).WithMany(z => z.Likes).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Like>().HasOne(c => c.Zweet).WithMany(z => z.Likes).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Like>().HasOne(c => c.Comment).WithMany(z => z.Likes).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rezweet>().HasOne(c => c.User).WithMany(z => z.Rezweets).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Rezweet>().HasOne(c => c.Zweet).WithMany(z => z.Rezweets).OnDelete(DeleteBehavior.NoAction);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            User[] users = new User[]
            {
                new User() { UserId = 1, UserName = "Admin", Email="Admin@gmail.com", HashedPassword = "", UserRole = UserRole.Admin, ProfileImagePath = "/ProfileImages/default.jpg", BackgroundImagePath = "/BackgroundImages/default.jpg", Bio = "Welcome to my Zwizzer profile! I'm an Admin!" },
                new User() { UserId = 2, UserName = "Sharon", Email="Sharon@gmail.com", HashedPassword = "", UserRole = UserRole.Subscriber, ProfileImagePath = "/ProfileImages/default.jpg", BackgroundImagePath = "/BackgroundImages/default.jpg", Bio = "Welcome to my Zwizzer profile! I'm a Subscriber!" },
                new User() { UserId = 3, UserName = "SickGamer420", Email="SickGamer420@gmail.com", HashedPassword = "", UserRole = UserRole.User, ProfileImagePath = "/ProfileImages/default.jpg", BackgroundImagePath = "/BackgroundImages/default.jpg", Bio = "Welcome to my Zwizzer profile! I'm a User!" },
            };
            var ph = new PasswordHasher<PasswordHashDAL>();
            var phDTO1 = new PasswordHashDAL(users[0]);
            var password1 = ph.HashPassword(phDTO1, "Admin");
            users[0].HashedPassword = password1;
            var phDTO2 = new PasswordHashDAL(users[1]);
            var password2 = ph.HashPassword(phDTO2, "VeryGood123");
            users[1].HashedPassword = password2;
            var phDTO3 = new PasswordHashDAL(users[2]);
            var password3 = ph.HashPassword(phDTO3, "TheBestEver");
            users[2].HashedPassword = password3;
            modelBuilder.Entity<User>().HasData(users);

            Zweet[] zweets = new Zweet[]
            {
                new Zweet() { ZweetId = 1, UserId = 1, Content = "This is the first ever Zweet on Zwizzer!", CreationTime = DateTime.Now.AddHours(-2) },
                new Zweet() { ZweetId = 2, UserId = 1, Content = "This is the second ever Zweet on Zwizzer! also, used as a test.", CreationTime = DateTime.Now.AddHours(-1.5) },
                new Zweet() { ZweetId = 3, UserId = 2, Content = "I heard of this about this new platform from my friends, guess im one of the firsts to use it!", CreationTime = DateTime.Now.AddHours(-1.1) },
                new Zweet() { ZweetId = 4, UserId = 2, Content = "So how's everybody day has been going?", CreationTime = DateTime.Now.AddHours(-1) },
                new Zweet() { ZweetId = 5, UserId = 3, Content = "Are there any games on this platform? I want to beat all the records on them!", CreationTime = DateTime.Now.AddHours(-0.5) },
                new Zweet() { ZweetId = 6, UserId = 3, Content = "Looking for group 4 World of Warcraft, any joiners?", CreationTime = DateTime.Now.AddHours(-0.47) },
            };
            modelBuilder.Entity<Zweet>().HasData(zweets);

            Like[] likes = new Like[]
            {
                new Like() { LikeId = 1, UserId = 1, ZweetId = 1, CommentId = null },
                new Like() { LikeId = 2, UserId = 2, ZweetId = 1, CommentId = null },
                new Like() { LikeId = 3, UserId = 3, ZweetId = 1, CommentId = null },
                new Like() { LikeId = 4, UserId = 1, ZweetId = 2, CommentId = null },
                new Like() { LikeId = 5, UserId = 2, ZweetId = 2, CommentId = null },
                new Like() { LikeId = 6, UserId = 1, ZweetId = 3, CommentId = null },
                new Like() { LikeId = 7, UserId = 2, ZweetId = 3, CommentId = null },
                new Like() { LikeId = 8, UserId = 3, ZweetId = 3, CommentId = null },
                new Like() { LikeId = 9, UserId = 1, ZweetId = 6, CommentId = null },
                new Like() { LikeId = 10, UserId = 2, ZweetId = 6, CommentId = null },
                new Like() { LikeId = 11, UserId = 1, CommentId = 1, ZweetId = null },
                new Like() { LikeId = 12, UserId = 2, CommentId = 1, ZweetId = null },
                new Like() { LikeId = 13, UserId = 2, CommentId = 2, ZweetId = null },
                new Like() { LikeId = 14, UserId = 3, CommentId = 2, ZweetId = null },
            };
            modelBuilder.Entity<Like>().HasData(likes);

            Comment[] comments = new Comment[]
            {
                new Comment() { CommentId = 1, UserId = 3, ZweetId = 4, Content = "What a great era to be alive, now that we have Zwizzer!", CreationTime = DateTime.Now.AddHours(-0.90) },
                new Comment() { CommentId = 2, UserId = 1, ZweetId = 4, Content = "Thank you for being the first testers of our lovely Social Media - Zwizzer!", CreationTime = DateTime.Now.AddHours(-0.86) },
            };
            modelBuilder.Entity<Comment>().HasData(comments);

            Rezweet[] rezweets = new Rezweet[]
            {
                new Rezweet() { RezweetId = 1, UserId = 1, ZweetId = 3 },
                new Rezweet() { RezweetId = 2, UserId = 1, ZweetId = 5 },
                new Rezweet() { RezweetId = 3, UserId = 3, ZweetId = 4 },
                new Rezweet() { RezweetId = 4, UserId = 2, ZweetId = 1 },
                new Rezweet() { RezweetId = 5, UserId = 3, ZweetId = 1 },
            };
            modelBuilder.Entity<Rezweet>().HasData(rezweets);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Models;

namespace WebApplication8.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData
              (
              new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
              new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }

              );
            modelBuilder.Entity<Post>()
           .HasOne(u => u.AppUser)
           .WithMany(a => a.posts)
           .HasForeignKey(u => u.AppUserId);

            modelBuilder.Entity<Comment>()
          .HasOne(u => u.Post)
          .WithMany(a => a.comments)
          .HasForeignKey(u => u.PostId);


        }
    }
    }

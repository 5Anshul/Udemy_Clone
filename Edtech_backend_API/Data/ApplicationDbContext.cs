using Edtech_backend_API.Identity;
using Edtech_backend_API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CourseVideo> CourseVideos { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<CourseInstructor>CourseInstructors { get; set; }
        public DbSet<CourseReview>CourseReviews { get; set; }
        public DbSet<Level>Levels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // one course can have many videos(One to many relationship)
            modelBuilder.Entity<CourseVideo>()
           .HasOne<Course>(s => s.Course)
           .WithMany(g => g.CourseVideos)
           .HasForeignKey(s => s.CourseId);

            //one course can have many reviews(one to many realtionship)
            modelBuilder.Entity<CourseReview>()
           .HasOne<Course>(s => s.Course)
           .WithMany(g => g.CourseReviews)
           .HasForeignKey(s => s.CourseId);

            // One category can have many Courses(One to many relationship)
            modelBuilder.Entity<Course>()
           .HasOne<Category>(s => s.Category)
           .WithMany(g => g.Courses)
           .HasForeignKey(s => s.CategoryId);

            //One Language can have many courses(one to many relationship)
            modelBuilder.Entity<Course>()
           .HasOne<Language>(s => s.Language)
           .WithMany(g => g.Courses)
           .HasForeignKey(s => s.LanguageId);

            //one CourseInstructor can have many courses(one to many relationship)
            modelBuilder.Entity<Course>()
           .HasOne<CourseInstructor>(s => s.CourseInstructor)
           .WithMany(g => g.Courses)
           .HasForeignKey(s => s.CourseInstructorId);

            //one Levle can have many courses(one to many relationship)
            modelBuilder.Entity<Course>()
           .HasOne<Level>(s => s.Level)
           .WithMany(g => g.Courses)
           .HasForeignKey(s => s.LevelId);

            //one ApplicationUser can have many CourseReviews(one to many relationship)
            modelBuilder.Entity<CourseReview>()
          .HasOne<ApplicationUser>(s => s.ApplicationUser)
          .WithMany(g => g.CourseReviews)
          .HasForeignKey(s => s.UserId);

            //one ApplicationUser can have many courses(one to many relationsship)
            modelBuilder.Entity<Course>()
          .HasOne<ApplicationUser>(s => s.ApplicationUser)
          .WithMany(g => g.Courses)
          .HasForeignKey(s => s.UserId);
        }
    }
}

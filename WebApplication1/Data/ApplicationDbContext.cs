using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Resource>()
                .HasOne(a => a.Category)
                .WithMany(a => a.Resources)
                .HasForeignKey(a => a.CategoryId);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Алкохол" },
                new Category { Id = 2, Name = "Наркотици" },
                new Category { Id = 3, Name = "Хазарт" },
                new Category { Id = 4, Name = "Общи" }
                );
        }
    }
    }


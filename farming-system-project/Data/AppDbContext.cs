using farming_system_project.Models;
using Microsoft.EntityFrameworkCore;

namespace farming_system_project.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Farmer> Farmer { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired();
            });

            // Farmer
            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired().HasMaxLength(100);
                entity.Property(f => f.Email).IsRequired().HasMaxLength(100);
                entity.Property(f => f.Password).IsRequired();
                entity.HasMany(f => f.Products)
                      .WithOne(p => p.Farmer)
                      .HasForeignKey(p => p.FarmerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Category).IsRequired().HasMaxLength(50);
                entity.Property(p => p.ProductionDate).IsRequired();
            });
        }
    }
}

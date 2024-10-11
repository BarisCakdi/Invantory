using Invantory.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Invantory.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Place> Places { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API
            //one-to-many ilişkisi
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Place)
                .WithMany(p => p.Items)
                .HasForeignKey(i => i.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Place>()
                .HasOne(p => p.Section)
                .WithMany(s => s.Places)
                .HasForeignKey(i => i.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Section>()
                .HasOne(s => s.Location)
                .WithMany(l => l.Sections)
                .HasForeignKey(s => s.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

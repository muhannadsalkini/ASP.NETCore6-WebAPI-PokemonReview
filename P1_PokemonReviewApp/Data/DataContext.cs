using Microsoft.EntityFrameworkCore;
using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Data
{
    public class DataContext : DbContext
    {
        // 
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        // Database Tables
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Pokemon-Category Many-To-Many Relationship
            modelBuilder.Entity<PokemonCategory>()
                .HasKey(pc => new { pc.PokemonId, pc.CategoryId });
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(c => c.CategoryId);

            // Pokemon-Owner Many-To-Many Relationship
            modelBuilder.Entity<PokemonOwner>()
                .HasKey(po => new { po.PokemonId, po.OwnerId });
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Owner)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(o => o.OwnerId);
        }
    }
}

// Type ctor and double tab to creat a constructer

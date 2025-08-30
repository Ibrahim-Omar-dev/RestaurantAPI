using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model.Entity;

namespace Restaurants.infrastructure.Data
{
    internal class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {
        internal DbSet<Restaurantt> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurantt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.OwnsOne(e => e.Address);

                entity.HasMany(e => e.Dishes)
                    .WithOne()
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

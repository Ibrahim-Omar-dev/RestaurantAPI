using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model.Entity;

namespace Restaurants.infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Restaurantt> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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

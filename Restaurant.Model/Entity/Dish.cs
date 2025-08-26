using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Model.Entity
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int? KiloCalories { get; set; }
        public int RestaurantId { get; set; }
    }
}

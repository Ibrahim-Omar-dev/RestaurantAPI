using AutoMapper;
using Restaurant.Model.Entity;

namespace Restaurant.Application.Dishes.Dtos
{
    class DishesMapper : Profile
    {
        public DishesMapper()
        {
            CreateMap<Dish, DishDto>().ReverseMap();
        }
    }
}

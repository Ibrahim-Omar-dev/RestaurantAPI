using AutoMapper;
using Restaurant.Application.Dishes.Command.CreateDishCommand;
using Restaurant.Model.Entity;

namespace Restaurant.Application.Dishes.Dtos
{
    class DishesMapper : Profile
    {
        public DishesMapper()
        {
            CreateMap<Dish, DishDto>().ReverseMap();
            CreateMap<Dish, CreateDishesCommand>().ReverseMap();
        }
    }
}

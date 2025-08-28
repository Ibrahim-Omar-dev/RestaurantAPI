using AutoMapper;
using Restaurant.Application.Command.CreateRestaurant;
using Restaurant.Application.Command.UpdateRestaurant;
using Restaurant.Model.Entity;

namespace Restaurant.Application.Restaurants.Dtos
{
    class RestaurantMapper : Profile
    {
        public RestaurantMapper()
        {
            CreateMap<Restaurantt, RestaurantDto>()
                 .ForMember(dest => dest.City, opt =>
                       opt.MapFrom(src => src.Address != null ? src.Address.City : null))
                 .ForMember(dest => dest.Street, opt =>
                       opt.MapFrom(src => src.Address != null ? src.Address.Street : null))
                 .ForMember(dest => dest.PostalCode, opt =>
                       opt.MapFrom(src => src.Address != null ? src.Address.PostalCode : null))
                 .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes))
                  .ReverseMap();

            CreateMap<CreateRestaurantCommand, Restaurantt>()
                .ForMember(d => d.Address, opt =>
                opt.MapFrom(src => new Address
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode
                })).ReverseMap();

            CreateMap<UpdateRestaurantCommand, Restaurantt>().ReverseMap();
        }
    }
}

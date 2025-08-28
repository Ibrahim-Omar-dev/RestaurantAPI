using AutoMapper;
using MediatR;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Model.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Queries.GetRestaurantById
{
    class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        private readonly IRestaurantRepository repository;
        private readonly IMapper mapper;

        public GetRestaurantByIdQueryHandler(IRestaurantRepository restaurant, IMapper mapper)
        {
            this.repository = restaurant;
            this.mapper = mapper;
        }
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await repository.GetById(request.Id);
            var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);

            return restaurantDto;
        }
    }
}

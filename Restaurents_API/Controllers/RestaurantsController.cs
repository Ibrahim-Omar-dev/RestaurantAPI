using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurants;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurants_API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRepositoryServices services;
        private readonly IMapper mapper;

        public RestaurantsController(IRepositoryServices services, IMapper mapper)
        {
            this.services = services;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurant = await services.GetAllAsync();
            var restaurantDto = mapper.Map<List<RestaurantDto>>(restaurant);
            return Ok(restaurantDto);
        }
        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetRestaurantById([FromRoute] int id)
        {
            var restaurant = await services.GetByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return Ok(restaurantDto);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            var newrestaurantId = await services.CreateAsync(createRestaurantDto);
            return Created(nameof(GetRestaurantById), new { id = newrestaurantId });
        }
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantDto updateRestaurantDto)
        {
            var existingRestaurant = await services.GetByIdAsync(id);
            if (existingRestaurant == null)
            {
                return NotFound();
            }
            var updatedRestaurant = await services.UpdateAsync(id, updateRestaurantDto);
            var updatedRestaurantDto = mapper.Map<RestaurantDto>(updatedRestaurant);
            return Ok(updatedRestaurantDto);
        }
    }
}
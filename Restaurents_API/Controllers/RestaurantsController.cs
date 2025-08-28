using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Command.CreateRestaurant;
using Restaurant.Application.Command.DeleteRestaurant;
using Restaurant.Application.Command.UpdateRestaurant;
using Restaurant.Application.Queries.GetAllRestaurant;
using Restaurant.Application.Queries.GetRestaurantById;
using Restaurant.Application.Restaurants;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurants_API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public RestaurantsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurant = await mediator.Send(new GetAllRestaurantQuery());
            return Ok(restaurant);
        }
        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetRestaurantById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurantCommand)
        {
            var newrestaurantId = await mediator.Send(createRestaurantCommand);
            return Created(nameof(GetRestaurantById), new { id = newrestaurantId });
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            var result = await mediator.Send(new DeleteRestaurantCommand(id));
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand updateRestaurantCommand)
        {
            updateRestaurantCommand.Id = id;
            var result = await mediator.Send(updateRestaurantCommand);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurants.Command.CreateRestaurant;
using Restaurant.Application.Restaurants.Command.DeleteRestaurant;
using Restaurant.Application.Restaurants.Command.UpdateRestaurant;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Application.Restaurants.Queries.GetAllRestaurant;
using Restaurant.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants_API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    [Authorize]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ILogger<RestaurantsController> logger;

        public RestaurantsController(IMediator mediator, IMapper mapper, ILogger<RestaurantsController> logger)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            logger.LogInformation("Fetching all restaurants at {Time}", DateTime.Now);
            var restaurant = await mediator.Send(new GetAllRestaurantQuery());
            return Ok(restaurant);
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<RestaurantDto?>> GetRestaurantById([FromRoute] int id)
        {
            logger.LogInformation("Fetching restaurant with Id {Id}", id);
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurantCommand)
        {
            var newRestaurantId = await mediator.Send(createRestaurantCommand);
            logger.LogInformation("Created restaurant with Id {Id}", newRestaurantId);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = newRestaurantId }, null);
        }

        [HttpDelete("Delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }

        [HttpPut("Update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantCommand updateRestaurantCommand)
        {
            updateRestaurantCommand.Id = id;
            await mediator.Send(updateRestaurantCommand);
            return NoContent();
        }
    }
}

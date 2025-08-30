using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes.Command.CreateDishCommand;
using Restaurant.Application.Dishes.Command.DeletDishCommand;
using Restaurant.Application.Dishes.Queries.GetAllDishes;
using Restaurant.Application.Dishes.Queries.GetDishesById;

namespace Restaurents_API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IMediator mediator;

        public DishesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromRoute] int restaurantId, CreateDishesCommand command)
        {
            command.RestaurantId = restaurantId;
            await mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] int restaurantId)
        {
            var result = await mediator.Send(new GetAllDishQuery(restaurantId));
            return Ok(result);
        }
        [HttpGet("{dishId}")] // api/restaurants/1/dishes/1
        public async Task<IActionResult> GetById([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var result = await mediator.Send(new GetDishesByIdQuery(restaurantId, dishId));
            return Ok(result);
        }
        [HttpDelete("{dishId}")]
        public async Task<IActionResult> Delete([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }
    }
}

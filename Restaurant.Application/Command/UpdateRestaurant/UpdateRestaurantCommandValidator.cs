using FluentValidation;
namespace Restaurant.Application.Command.UpdateRestaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(c => c.Name).Length(3, 100);
        }
    }
}

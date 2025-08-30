using FluentValidation;

namespace Restaurant.Application.Restaurants.Command.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = new()
        {
            "Italian", "Mexican", "Japanese", "American", "Indian"
        };

        public CreateRestaurantCommandValidator()
        {
            RuleFor(c => c.Category)
                .Must(validCategories.Contains)
                .WithMessage("Invalid category. Please choose from the valid categories.");

            RuleFor(c => c.Name)
                .Length(3, 100);

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Please provide a valid postal code (XX-XXX).");
        }
    }
}


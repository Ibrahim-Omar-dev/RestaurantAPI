using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurant
{
    class GetAllRestaurantValidation : AbstractValidator<GetAllRestaurantQuery>
    {
        private readonly List<int> AllowancePageSize = new() { 3, 5, 20, 30 };
        public GetAllRestaurantValidation()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");

            RuleFor(r => r.PageSize)
        .Must(size => AllowancePageSize.Contains(size))
        .WithMessage($"Page size must be one of the following values: {string.Join(", ", AllowancePageSize)}");

        }
    }
}

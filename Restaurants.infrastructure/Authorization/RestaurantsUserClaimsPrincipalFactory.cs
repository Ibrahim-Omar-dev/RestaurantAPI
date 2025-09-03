using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurant.Model.Constant;
using Restaurant.Model.Entity;
using Restaurants.infrastructure.Authorization.Constant;
using System.Security.Claims;

namespace Restaurants.infrastructure.Authorization;

public class RestaurantsUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roles;
    private readonly IOptions<IdentityOptions> options;

    public RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager,
        RoleManager<IdentityRole> roles, IOptions<IdentityOptions> options)
        : base(userManager, roles, options)
    {
        this.userManager = userManager;
        this.roles = roles;
        this.options = options;
    }
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);
        if (!string.IsNullOrEmpty(user.Nationality))
        {
            id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
        }
        if (user.BirthDate != null)
        {
            id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.BirthDate.Value.ToShortDateString()));
        }
        return new ClaimsPrincipal(id);
    }
}

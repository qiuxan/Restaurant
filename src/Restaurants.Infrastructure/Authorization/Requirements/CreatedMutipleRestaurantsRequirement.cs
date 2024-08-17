
using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreatedMutipleRestaurantsRequirement(int minimumRestaurants) : IAuthorizationRequirement
{
    public int MinimumRestaurants { get; } = minimumRestaurants;
}

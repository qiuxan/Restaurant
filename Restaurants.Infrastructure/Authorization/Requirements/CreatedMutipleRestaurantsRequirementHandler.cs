
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

internal class CreatedMutipleRestaurantsRequirementHandler(
    IUserContext userContext,
    IRestaurantsRepository restaurantsRepository
    )
    : AuthorizationHandler<CreatedMutipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMutipleRestaurantsRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        var restaurants = await restaurantsRepository.GetAllAsync();

        var userCreatedRestaurants = restaurants.Count(r=>r.OwnerId == currentUser!.Id);

        if (userCreatedRestaurants >= requirement.MinimumRestaurants)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

    }
}

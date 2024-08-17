using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler(
    ILogger<MinimumAgeRequirementHandler> logger,
    IUserContext userContext
    ) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("User : {Email} , Date of Birth {DoB} - Handling MinimumAgeRequirement", 
            currentUser.Email, currentUser.DateOfBirth);

        if (currentUser.DateOfBirth == null)
        {
            logger.LogInformation("Authorization Failed beause the date of birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        { 
            logger.LogInformation("Authorization Successed");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("Authorization Failed because the user is not old enough");
            context.Fail();
        }
        return Task.CompletedTask;
        
    }
}

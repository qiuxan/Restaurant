﻿

using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.User;

public interface IUserContext
{
    CurrentUser? CurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? CurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;

        if (user == null) throw new InvalidCastException("User is not authenticated");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
            return null;

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);


        return new CurrentUser(userId, email, roles);

    }
}

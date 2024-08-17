using FluentAssertions;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class CurrentUserTests
{
    // name convention: TestMethodName_Scenario_ExpectedBehavior
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        //arrange
        var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User],null, null );

        //act

        var isInRole = user.IsInRole(roleName);
        //assert

        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        //arrange
        var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        //act

        var isInRole = user.IsInRole(UserRoles.Owner);
        //assert

        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        //arrange
        var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        //act

        var isInRole = user.IsInRole(UserRoles.Admin.ToLower());
        //assert

        isInRole.Should().BeFalse();
    }
}
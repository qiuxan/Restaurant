using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Restaurants.Domain.Constants;
using System.Security.Claims;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        // name convention: TestMethodName_Scenario_ExpectedBehavior

        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            //arrange
            var dateOfBirth = new DateOnly(1990, 1, 1);


            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, "1"),
                new(ClaimTypes.Email, "test@test.com"),
                new(ClaimTypes.Role, UserRoles.Admin),
                new(ClaimTypes.Role, UserRoles.User),
                new("Nationality", "German"),
                new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims,"Test")); // the second argument should be anything but null

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext() { User = user});

            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act 

            var currentUser = userContext.GetCurrentUser();

            // assert

            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentUser.Nationality.Should().Be("German");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);

        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            //arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act
            Action action = () => userContext.GetCurrentUser();
            
            // assert

            action.Should().Throw<InvalidCastException>().WithMessage("User is not authenticated");



        }
    }
}
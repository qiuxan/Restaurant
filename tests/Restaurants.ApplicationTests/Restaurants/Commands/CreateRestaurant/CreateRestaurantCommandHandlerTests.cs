using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantIdAsync()
    {
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

        var mapperMock = new Mock<IMapper>();

        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();
        mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);


        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();

        restaurantsRepositoryMock.Setup(x => x.Create(It.IsAny<Restaurant>())).ReturnsAsync(1);

        var userContextMock = new Mock<IUserContext>();

        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);



        var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object, mapperMock.Object, restaurantsRepositoryMock.Object, userContextMock.Object);


        // Act

        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantsRepositoryMock.Verify(r => r.Create(restaurant), Times.Once);

    }
}
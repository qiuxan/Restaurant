using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Policy;
using Restaurants.APITests;
using Moq;
using Restaurants.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurants.Domain.Entities;
using System.Net.Http.Json;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers.Tests;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{

    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();

    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder => { 
            builder.ConfigureTestServices(services => {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository),
                                                                 _=>_restaurantsRepositoryMock.Object));
            });
            
        });
    }


    [Fact]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        // arrange
        var id = 11111;
        var client = _factory.CreateClient();

        _restaurantsRepositoryMock.Setup(x => x.GetRestaurantById(id)).ReturnsAsync((Restaurant?)null);

        // act
        var response = await client.GetAsync($"/api/restaurants/{id}");

        // assert

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

    }


    [Fact]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        // arrange
        var id = 99;
        var client = _factory.CreateClient();

        Restaurant restaurant = new Restaurant()
        {
            Id = id,
            Name = "Test Restaurant",
            Description = "Test Description",
        };

        _restaurantsRepositoryMock.Setup(x => x.GetRestaurantById(id)).ReturnsAsync(restaurant);

        // act
        var response = await client.GetAsync($"/api/restaurants/{id}");
        var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

        // assert

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be("Test Restaurant");
        restaurantDto.Description.Should().Be("Test Description");

    }

    [Fact()]

    public async Task GetAll_ForValidRequest_Returns200Ok()
    {
        //arrange
        var client = _factory.CreateClient();

        //act
        var response = await client.GetAsync("/api/restaurants?pageSize=15&&pageNumber=1");

        //assert

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK );


    }

    [Fact]
    public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var result = await client.GetAsync("/api/restaurants");

        // assert

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

    }
}
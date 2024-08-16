﻿using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;

namespace Restaurants.API.Controllers.Tests;

public class RestaurantsControllerTests:IClassFixture<WebApplicationFactory<Program>>
{

    private readonly WebApplicationFactory<Program> _factory;

    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
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
﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsServices(
        IRestaurantsRepository restaurantsRepository,
        ILogger<RestaurantsServices> logger,
        IMapper mapper) : IRestaurantsServices
    {
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();

            var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantDtos!;
        }

        public async Task<RestaurantDto?> GetRestaurantById(int id)
        {
            logger.LogInformation($"Getting restaurant by id {id}");
            var restaurant = await restaurantsRepository.GetRestaurantById(id); 
            var restaurantDto =  mapper.Map<RestaurantDto?>(restaurant);

            return restaurantDto;
        }
    }
}

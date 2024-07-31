using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsServices(
        IRestaurantsRepository restaurantsRepository,
        ILogger<RestaurantsServices> logger) : IRestaurantsServices
    {
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();

            var restaurantDtos = restaurants.Select(RestaurantDto.FromEntity);
            return restaurantDtos!;
        }

        public async Task<RestaurantDto?> GetRestaurantById(int id)
        {
            logger.LogInformation($"Getting restaurant by id {id}");
            var restaurant = await restaurantsRepository.GetRestaurantById(id);

            var restaurantDto = RestaurantDto.FromEntity(restaurant);
            return restaurantDto;
        }
    }
}

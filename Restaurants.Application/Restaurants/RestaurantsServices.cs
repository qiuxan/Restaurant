using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsServices(
        IRestaurantsRepository restaurantsRepository,
        ILogger<RestaurantsServices> logger) : IRestaurantsServices
    {
        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();
            return restaurants;
        }
    }
}

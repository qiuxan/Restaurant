using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsServices
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
        Task<RestaurantDto?> GetRestaurantById(int id);
    }
}
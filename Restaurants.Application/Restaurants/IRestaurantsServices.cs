using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsServices
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
    }
}
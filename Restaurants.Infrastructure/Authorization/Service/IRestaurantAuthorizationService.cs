using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization.Service
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}
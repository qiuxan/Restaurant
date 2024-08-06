

using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Commands.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQuery(int restaurantId, int dishId):IRequest<DishDto>
{

    public int RestaurantId { get; } = restaurantId;
    public int DishId { get; }  = dishId;
}

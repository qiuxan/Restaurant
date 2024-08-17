
using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant;

public class DeleteDishesForRestaurantCommand(int RestaurantId) : IRequest
{
    public int RestaurantId { get; } = RestaurantId;
}

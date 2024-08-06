
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(
    ILogger<GetDishesForRestaurantQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository
    ) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all dishes for restaurant: {RestaurantId}", request.RestaurantId);
        var restaurant = await restaurantsRepository.GetRestaurantById(request.RestaurantId);

        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dishDtos = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

        return dishDtos;

    }
}
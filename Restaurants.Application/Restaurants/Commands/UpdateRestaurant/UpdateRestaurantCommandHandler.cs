using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
    ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantRepository,
    IMapper mapper
    ) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating restaurant with id {request.Id}");
        var restaurant = await restaurantRepository.GetRestaurantById(request.Id);
        if (restaurant is null)
        {
            logger.LogWarning($"Restaurant with id {request.Id} not found");
            return false;
        }

        mapper.Map(request, restaurant);
        //restaurant.Name = request.Name;
        //restaurant.Description = request.Description;
        //restaurant.HasDelivery = request.HasDelivery;

        await restaurantRepository.SaveChanges();
        return true;
    }
}

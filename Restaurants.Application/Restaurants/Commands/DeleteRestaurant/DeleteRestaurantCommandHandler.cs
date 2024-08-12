using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(
    ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService
    ) 
    : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting restaurant with id {request.Id}");
        var restaurant = await restaurantRepository.GetRestaurantById(request.Id);
        if (restaurant is null)
        {
            logger.LogWarning($"Restaurant with id {request.Id} not found");
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            logger.LogWarning($"User is not authorized to delete restaurant with id {request.Id}");
            throw new ForbidException();
        }

        await restaurantRepository.SaveChanges();
         
    }
}

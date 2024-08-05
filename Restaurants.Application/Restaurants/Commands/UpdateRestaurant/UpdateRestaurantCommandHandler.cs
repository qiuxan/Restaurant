﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
    ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantRepository,
    IMapper mapper
    ) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant with id:{Restaurant} with {@UpdatedRestaurant}", request.Id, request);
        var restaurant = await restaurantRepository.GetRestaurantById(request.Id);
        if (restaurant is null)
        {
            logger.LogWarning($"Restaurant with id {request.Id} not found");
            throw new NotFoundException($"Restaurant with id {request.Id} not found");
        }

        mapper.Map(request, restaurant);
        //restaurant.Name = request.Name;
        //restaurant.Description = request.Description;
        //restaurant.HasDelivery = request.HasDelivery;

        await restaurantRepository.SaveChanges();
    }
}

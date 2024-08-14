
using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator: AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowPageSize = [5,10,15,30];

    private string[] allowedSortByColumnName = [
        nameof(RestaurantDto.Name),
        nameof(RestaurantDto.Category),
        nameof(RestaurantDto.Description),
        ];
    public GetAllRestaurantsQueryValidator()
    { 
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .Must(x => allowPageSize.Contains(x)).WithMessage($"Page size must be [{string.Join(",", allowPageSize)}]");

        RuleFor(x => x.SortBy)
            .Must(x => allowedSortByColumnName.Contains(x))
            .When(x => !string.IsNullOrEmpty(x.SortBy))
            .WithMessage($"Sort by column name must be [{string.Join(",", allowedSortByColumnName)}]");
    }   
}

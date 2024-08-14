
using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator: AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowPageSize = [5,10,15,30];
    public GetAllRestaurantsQueryValidator()
    { 
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .Must(x => allowPageSize.Contains(x)).WithMessage($"Page size must be [{string.Join(",", allowPageSize)}]")

    }   
}

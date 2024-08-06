
using FluentValidation;

namespace Restaurants.Application.Dishes.Command.CreateDish;

public class CreateDishCommandValidator: AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.KiloCalories)
            .GreaterThanOrEqualTo(0).WithMessage("KiloCalories must be greater than or equal to 0");
    }
}

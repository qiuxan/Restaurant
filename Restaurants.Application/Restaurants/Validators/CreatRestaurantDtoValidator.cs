using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Validators;

public class CreatRestaurantDtoValidator: AbstractValidator<CreateRestaurantDto>
{
    public CreatRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);
        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required!");

        RuleFor(dto => dto.Category)
            .NotEmpty().WithMessage("Insert a valid category");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Please provide an email address!");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$").WithMessage("Postal code must be in the format 00-000");
    }
}

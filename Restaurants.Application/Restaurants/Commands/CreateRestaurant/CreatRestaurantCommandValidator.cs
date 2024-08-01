using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreatRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validaCategories = ["Italian", "Polish", "Mexican", "Chinese", "Indian", "Japanese", "Thai"];
    public CreatRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);


        RuleFor(dto => dto.Category)
            .Must(category => validaCategories.Contains(category))
            .WithMessage("Category must be one of the following: Italian, Polish, Mexican, Chinese, Indian, Japanese, Thai");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Please provide an email address!");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$").WithMessage("Postal code must be in the format 00-000");
    }
}

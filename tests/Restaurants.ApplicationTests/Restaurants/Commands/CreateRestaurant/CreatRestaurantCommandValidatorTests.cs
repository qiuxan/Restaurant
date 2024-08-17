using FluentValidation.TestHelper;
using Xunit;


namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreatRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        //arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
            Category = "Italian",
            ContactEmail = "test@test.com",
            PostalCode = "12-345",
        };

        var validator = new CreateRestaurantCommandValidator();

        //act
        var result = validator.TestValidate(command);

        //assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        //arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "T",
            Category = "I",
            ContactEmail = "est.com",
            PostalCode = "345",
        };

        var validator = new CreateRestaurantCommandValidator();

        //act
        var result = validator.TestValidate(command);

        //assert

        result.ShouldHaveValidationErrorFor(x => x.Name);
        result.ShouldHaveValidationErrorFor(x => x.Category);
        result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        result.ShouldHaveValidationErrorFor(x => x.PostalCode);
    }

    //"Italian", "Polish", "Mexican", "Chinese", "Indian", "Japanese", "Thai"
    [Theory()]
    [InlineData("Italian")]
    [InlineData("Polish")]
    [InlineData("Mexican")]
    [InlineData("Chinese")]
    [InlineData("Indian")]
    [InlineData("Japanese")]
    [InlineData("Thai")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        // arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { Category = category };

        // act

        var result = validator.TestValidate(command);

        // assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);

    }

    [Theory()]
    [InlineData("10220")]
    [InlineData("102-20")]
    [InlineData("10 220")]
    [InlineData("10-2 20")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        // arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { PostalCode = postalCode };

        // act

        var result = validator.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}
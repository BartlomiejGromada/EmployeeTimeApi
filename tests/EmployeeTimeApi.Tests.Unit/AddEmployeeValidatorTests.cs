using EmployeeTimeApi.Application.Employees.Validators;
using FluentValidation.TestHelper;

namespace EmployeeTimeApi.Tests.Unit;

public class AddEmployeeValidatorTests
{
    [Fact]
    public void Validation_For_Given_AddEmployeeDto_Should_Be_Successful()
    {
        // Arrange
        var validator = new AddEmployeeValidator();
        var dto = new Application.Employees.Dtos.AddEmployeeDto("Jan", "Kowalski", "jan.kowalski@example.com");

        // Act
        var response = validator.TestValidate(dto);

        // Assert
        response.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validation_For_Given_AddEmployeeDto_Should_End_With_Errors()
    {
        // Arrange
        var validator = new AddEmployeeValidator();
        var dto = new Application.Employees.Dtos.AddEmployeeDto(string.Empty, string.Empty, string.Empty);

        // Act
        var response = validator.TestValidate(dto);

        // Assert
        response.ShouldHaveAnyValidationError();
        response.ShouldHaveValidationErrorFor(x => x.FirstName);
        response.ShouldHaveValidationErrorFor(x => x.LastName);
        response.ShouldHaveValidationErrorFor(x => x.Email);
    }
}
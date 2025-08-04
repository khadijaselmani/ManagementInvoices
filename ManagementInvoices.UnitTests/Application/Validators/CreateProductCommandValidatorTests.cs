using ManagementInvoices.Application.Features.Products.Commands.CreateProduct;

namespace ManagementInvoices.UnitTests.Application.Validators
{
    public class CreateProductCommandValidatorTests
    {
        private readonly CreateProductCommandValidator _validator;

        public CreateProductCommandValidatorTests()
        {
            _validator = new CreateProductCommandValidator();
        }

        [Fact]
        public void Validate_WithValidCommand_ShouldPass()
        {
            // Arrange
            var command = new CreateProductCommand("Valid Product", 99.99m);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Validate_WithInvalidName_ShouldFail(string invalidName)
        {
            // Arrange
            var command = new CreateProductCommand(invalidName, 99.99m);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(command.Name));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Validate_WithEmptyName_ShouldReturnCorrectErrorMessage(string invalidName)
        {
            // Arrange
            var command = new CreateProductCommand(invalidName, 99.99m);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.Errors.Should().ContainSingle(e => 
                e.PropertyName == nameof(command.Name) && 
                e.ErrorMessage == "Product name is required.");
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("This is a very long product name that exceeds the maximum allowed length of one hundred characters and should fail validation")]
        public void Validate_WithNameTooLong_ShouldFail(string longName)
        {
            // Arrange
            var command = new CreateProductCommand(longName, 99.99m);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => 
                e.PropertyName == nameof(command.Name) && 
                e.ErrorMessage == "Product name must be less than 100 characters.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Validate_WithInvalidPrice_ShouldFail(decimal invalidPrice)
        {
            // Arrange
            var command = new CreateProductCommand("Valid Product", invalidPrice);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(command.Price));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Validate_WithInvalidPrice_ShouldReturnCorrectErrorMessage(decimal invalidPrice)
        {
            // Arrange
            var command = new CreateProductCommand("Valid Product", invalidPrice);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.Errors.Should().ContainSingle(e => 
                e.PropertyName == nameof(command.Price) && 
                e.ErrorMessage == "Price must be greater than 0.");
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(1)]
        [InlineData(99.99)]
        [InlineData(1000)]
        public void Validate_WithValidPrice_ShouldPass(decimal validPrice)
        {
            // Arrange
            var command = new CreateProductCommand("Valid Product", validPrice);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WithMultipleValidationErrors_ShouldReturnAllErrors()
        {
            // Arrange
            var command = new CreateProductCommand("", -1);

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(2);
            result.Errors.Should().Contain(e => e.PropertyName == nameof(command.Name));
            result.Errors.Should().Contain(e => e.PropertyName == nameof(command.Price));
        }
    }
} 
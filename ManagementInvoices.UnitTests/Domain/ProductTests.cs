using ManagementInvoices.Domain.Entities;

namespace ManagementInvoices.UnitTests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateProduct()
        {
            // Arrange
            var name = "Test Product";
            var price = 99.99m;

            // Act
            var product = new Product(name, price);

            // Assert
            product.Should().NotBeNull();
            product.Name.Should().Be(name);
            product.Price.Should().Be(price);
            product.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void Constructor_WithValidParameters_ShouldGenerateUniqueId()
        {
            // Arrange & Act
            var product1 = new Product("Product 1", 10.00m);
            var product2 = new Product("Product 2", 20.00m);

            // Assert
            product1.Id.Should().NotBe(product2.Id);
        }

        [Fact]
        public void Update_WithValidParameters_ShouldUpdateProduct()
        {
            // Arrange
            var product = new Product("Original Name", 10.00m);
            var newName = "Updated Name";
            var newPrice = 25.50m;

            // Act
            product.Update(newName, newPrice);

            // Assert
            product.Name.Should().Be(newName);
            product.Price.Should().Be(newPrice);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Constructor_WithInvalidName_ShouldThrowArgumentException(string invalidName)
        {
            // Act & Assert
            var action = () => new Product(invalidName, 10.00m);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithNegativePrice_ShouldThrowArgumentException(decimal negativePrice)
        {
            // Act & Assert
            var action = () => new Product("Test Product", negativePrice);
            action.Should().Throw<ArgumentException>();
        }
    }
} 
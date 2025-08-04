using ManagementInvoices.Application.Features.Products.Commands.CreateProduct;
using ManagementInvoices.Application.Interfaces;
using ManagementInvoices.Domain.Entities;

namespace ManagementInvoices.UnitTests.Application.Commands
{
    public class CreateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldCreateProduct()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            var products = new List<Product>();
            mockProductsDbSet.Setup(d => d.Add(It.IsAny<Product>()))
                .Callback<Product>(product => products.Add(product));
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var handler = new CreateProductCommandHandler(mockContext.Object);
            var command = new CreateProductCommand("Test Product", 99.99m);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty();
            products.Should().HaveCount(1);
            products[0].Name.Should().Be("Test Product");
            products[0].Price.Should().Be(99.99m);
            
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldReturnProductId()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            var products = new List<Product>();
            mockProductsDbSet.Setup(d => d.Add(It.IsAny<Product>()))
                .Callback<Product>(product => products.Add(product));
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var handler = new CreateProductCommandHandler(mockContext.Object);
            var command = new CreateProductCommand("Test Product", 99.99m);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(products[0].Id);
        }

        [Fact]
        public async Task Handle_WhenSaveChangesFails_ShouldPropagateException()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Database error"));

            var handler = new CreateProductCommandHandler(mockContext.Object);
            var command = new CreateProductCommand("Test Product", 99.99m);

            // Act & Assert
            var action = () => handler.Handle(command, CancellationToken.None);
            await action.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Database error");
        }
    }
} 
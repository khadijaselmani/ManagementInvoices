using ManagementInvoices.Application.Features.Products.Queries;
using ManagementInvoices.Application.Interfaces;
using ManagementInvoices.Domain.Entities;

namespace ManagementInvoices.UnitTests.Application.Queries
{
    public class GetProductByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_WithExistingProduct_ShouldReturnProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var expectedProduct = new Product("Test Product", 99.99m);

            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            mockProductsDbSet.Setup(d => d.FindAsync(
                It.Is<object[]>(ids => ids.Contains(productId)), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedProduct);
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);

            var handler = new GetProductByIdQueryHandler(mockContext.Object);
            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(expectedProduct);
        }

        [Fact]
        public async Task Handle_WithNonExistentProduct_ShouldReturnNull()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            mockProductsDbSet.Setup(d => d.FindAsync(
                It.Is<object[]>(ids => ids.Contains(productId)), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product)null);
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);

            var handler = new GetProductByIdQueryHandler(mockContext.Object);
            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_WhenDatabaseThrowsException_ShouldPropagateException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            mockProductsDbSet.Setup(d => d.FindAsync(
                It.Is<object[]>(ids => ids.Contains(productId)), 
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Database error"));
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);

            var handler = new GetProductByIdQueryHandler(mockContext.Object);
            var query = new GetProductByIdQuery(productId);

            // Act & Assert
            var action = () => handler.Handle(query, CancellationToken.None);
            await action.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Database error");
        }
    }
} 
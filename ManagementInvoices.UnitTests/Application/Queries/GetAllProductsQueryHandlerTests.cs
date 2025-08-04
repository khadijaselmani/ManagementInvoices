using ManagementInvoices.Application.Features.Products.Queries;
using ManagementInvoices.Application.Interfaces;
using ManagementInvoices.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementInvoices.UnitTests.Application.Queries
{
    public class GetAllProductsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_WithExistingProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product("Product 1", 10.00m),
                new Product("Product 2", 20.00m),
                new Product("Product 3", 30.00m)
            };

            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            mockProductsDbSet.Setup(d => d.ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);

            var handler = new GetAllProductsQueryHandler(mockContext.Object);
            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task Handle_WithNoProducts_ShouldReturnEmptyList()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            mockProductsDbSet.Setup(d => d.ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Product>());
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);

            var handler = new GetAllProductsQueryHandler(mockContext.Object);
            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_WhenDatabaseThrowsException_ShouldPropagateException()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            mockProductsDbSet.Setup(d => d.ToListAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Database error"));
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);

            var handler = new GetAllProductsQueryHandler(mockContext.Object);
            var query = new GetAllProductsQuery();

            // Act & Assert
            var action = () => handler.Handle(query, CancellationToken.None);
            await action.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Database error");
        }
    }
} 
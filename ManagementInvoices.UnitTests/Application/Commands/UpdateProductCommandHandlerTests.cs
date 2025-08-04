using ManagementInvoices.Application.Features.Products.Commands.UpdateProduct;
using ManagementInvoices.Application.Interfaces;
using ManagementInvoices.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ManagementInvoices.UnitTests.Application.Commands
{
    public class UpdateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldUpdateProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = new Product("Original Name", 10.00m);
            
            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            mockProductsDbSet.Setup(d => d.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Product, bool>>>(), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingProduct);
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var handler = new UpdateProductCommandHandler(mockContext.Object);
            var command = new UpdateProductCommand(productId, "Updated Name", 25.50m);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            existingProduct.Name.Should().Be("Updated Name");
            existingProduct.Price.Should().Be(25.50m);
            
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNonExistentProduct_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            mockProductsDbSet.Setup(d => d.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Product, bool>>>(), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product)null);
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);

            var handler = new UpdateProductCommandHandler(mockContext.Object);
            var command = new UpdateProductCommand(productId, "Updated Name", 25.50m);

            // Act & Assert
            var action = () => handler.Handle(command, CancellationToken.None);
            await action.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Product with ID '{productId}' not found.");
        }

        [Fact]
        public async Task Handle_WhenSaveChangesFails_ShouldPropagateException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = new Product("Original Name", 10.00m);
            
            var mockContext = new Mock<IApplicationDbContext>();
            var mockProductsDbSet = new Mock<DbSet<Product>>();
            
            mockProductsDbSet.Setup(d => d.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Product, bool>>>(), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingProduct);
            
            mockContext.Setup(c => c.Products).Returns(mockProductsDbSet.Object);
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Database error"));

            var handler = new UpdateProductCommandHandler(mockContext.Object);
            var command = new UpdateProductCommand(productId, "Updated Name", 25.50m);

            // Act & Assert
            var action = () => handler.Handle(command, CancellationToken.None);
            await action.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Database error");
        }
    }
} 
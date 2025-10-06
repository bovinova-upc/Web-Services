using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp.Tests.UnitTests
{
    public class ProductTests
    {
        [Fact]
        public void CreateProduct_WithExpirationDate()
        {
            // Arrange
            var expiration = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            var command = new CreateProductCommand("Producto Expira", 3, 50, 2, expiration);

            // Act
            var product = new Product(command);

            // Assert
            Assert.Equal("Producto Expira", product.Name);
            Assert.Equal(3, product.CategoryId);
            Assert.Equal(50, product.Quantity);
            Assert.Equal(2, product.UserId);
            Assert.Equal(expiration, product.ExpirationDate);
        }

        [Fact]
        public void CreateProduct_WithoutExpirationDate()
        {
            // Arrange
            var command = new CreateProductCommand("Producto Sin Expiración", 4, 20, 3, null);

            // Act
            var product = new Product(command);

            // Assert
            Assert.Equal("Producto Sin Expiración", product.Name);
            Assert.Equal(4, product.CategoryId);
            Assert.Equal(20, product.Quantity);
            Assert.Equal(3, product.UserId);
            Assert.Null(product.ExpirationDate);
        }

        [Fact]
        public void UpdateProduct_ValidData()
        {
            // Arrange
            var product = new Product("Producto 2", 3, 50, 2);
            var updateCommand = new UpdateProductCommand(1, "Producto Actualizado", 4, 75, null);

            // Act
            product.Update(updateCommand);

            // Assert
            Assert.Equal("Producto Actualizado", product.Name);
            Assert.Equal(4, product.CategoryId);
            Assert.Equal(75, product.Quantity);
            Assert.Null(product.ExpirationDate);
        }
    }
}

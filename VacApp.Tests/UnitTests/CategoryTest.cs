using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp.Tests.UnitTests
{
    public class CategoryTests
    {
        [Fact]
        public void CreateCategory_ValidData()
        {
            // Arrange
            var command = new CreateCategoryCommand("Categoria 1", 1);

            // Act
            var category = new Category(command);

            // Assert
            Assert.Equal("Categoria 1", category.Name);
            Assert.Equal(1, category.UserId);
        }

        [Fact]
        public void UpdateCategory_ValidData()
        {
            // Arrange
            var category = new Category("Categoria 2", 2);
            var updateCommand = new UpdateCategoryCommand(1, "Categoria Actualizada");

            // Act
            category.Update(updateCommand);

            // Assert
            Assert.Equal("Categoria Actualizada", category.Name);
        }
    }
}

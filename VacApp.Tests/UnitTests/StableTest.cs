using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp.Tests.UnitTests
{
    public class StableTests
    {
        [Fact]
        public void CreateStable_ValidData()
        {
            // Arrange
            var command = new CreateStableCommand("Establo 1", 10, 1);

            // Act
            var stable = new Stable(command);

            // Assert
            Assert.Equal("Establo 1", stable.Name);
            Assert.Equal(10, stable.Limit);
            Assert.Equal(1, stable.UserId);
        }

        [Fact]
        public void CreateStable_InvalidLimit()
        {
            // Arrange
            var command = new CreateStableCommand("Establo 2", 0, 2);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Stable(command));
        }

        [Fact]
        public void CreateStable_EmptyName()
        {
            // Arrange
            var command = new CreateStableCommand("", 5, 3);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Stable(command));
        }

        [Fact]
        public void UpdateStable_ValidData()
        {
            // Arrange
            var stable = new Stable(new CreateStableCommand("Establo 3", 5, 4));
            var updateCommand = new UpdateStableCommand(1, "Establo Actualizado", 20);

            // Act
            stable.Update(updateCommand);

            // Assert
            Assert.Equal("Establo Actualizado", stable.Name);
            Assert.Equal(20, stable.Limit);
        }

        [Fact]
        public void UpdateStable_InvalidLimit()
        {
            // Arrange
            var stable = new Stable(new CreateStableCommand("Establo 4", 5, 5));
            var updateCommand = new UpdateStableCommand(1, "Establo 4", 0);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => stable.Update(updateCommand));
        }
    }
}

using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;

namespace VacApp.Tests.UnitTests
{
    public class StaffTests
    {
        [Fact]
        public void CreateStaff_ValidData()
        {
            // Arrange
            var command = new CreateStaffCommand("Juan Perez", 1, 10);

            // Act
            var staff = new Staff(command);

            // Assert
            Assert.Equal("Juan Perez", staff.Name);
            Assert.Equal(1, staff.EmployeeStatus.Value);
            Assert.Equal(10, staff.UserId);
        }

        [Fact]
        public void CreateStaff_InvalidEmployeeStatus()
        {
            // Arrange
            var command = new CreateStaffCommand("Ana Lopez", 99, 20);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Staff(command));
        }

        [Fact]
        public void UpdateStaff_FullData()
        {
            // Arrange
            var staff = new Staff("Pedro", 1, 5);
            var updateCommand = new UpdateStaffCommand(1, "Pedro Actualizado", 2);

            // Act
            staff.Update(updateCommand);

            // Assert
            Assert.Equal("Pedro Actualizado", staff.Name);
            Assert.Equal(2, staff.EmployeeStatus.Value);
        }

        [Fact]
        public void UpdateStaff_InvalidEmployeeStatus()
        {
            // Arrange
            var staff = new Staff("Maria", 1, 7);
            var updateCommand = new UpdateStaffCommand(1, "Maria", 0);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => staff.Update(updateCommand));
        }
    }
}

using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp.Tests.UnitTests
{
    public class BovineTests
    {
        [Fact]
        public void CreateBovine_ValidData()
        {
            // Arrange
            var birthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-2));
            var command = new CreateBovineCommand("Bovi", "male", birthDate, "Angus", 1, "img.jpg", 10, null);

            // Act
            var bovine = new Bovine(command);

            // Assert
            Assert.Equal("Bovi", bovine.Name);
            Assert.Equal("male", bovine.Gender);
            Assert.Equal(birthDate, bovine.BirthDate);
            Assert.Equal("Angus", bovine.Breed);
            Assert.Equal(1, bovine.StableId);
            Assert.Equal("img.jpg", bovine.BovineImg);
            Assert.Equal(10, bovine.UserId);
        }

        [Fact]
        public void CreateBovine_InvalidGender()
        {
            // Arrange
            var birthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-2));
            var command = new CreateBovineCommand("Bovi", "other", birthDate, "Angus", 1, "img.jpg", 10, null);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Bovine(command));
        }

        [Fact]
        public void UpdateBovine_ValidData()
        {
            // Arrange
            var birthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-2));
            var bovine = new Bovine("Bovi", "male", birthDate, "Angus", 1, "img.jpg", 10);
            var newBirthDate = birthDate.AddYears(-1);
            var updateCommand = new UpdateBovineCommand(1, "BoviActualizado", "female", newBirthDate, "Hereford", null, 2);

            // Act
            bovine.Update(updateCommand);

            // Assert
            Assert.Equal("BoviActualizado", bovine.Name);
            Assert.Equal("female", bovine.Gender);
            Assert.Equal(newBirthDate, bovine.BirthDate);
            Assert.Equal("Hereford", bovine.Breed);
            Assert.Equal(2, bovine.StableId);
        }

        [Fact]
        public void UpdateBovine_InvalidGender()
        {
            // Arrange
            var birthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-2));
            var bovine = new Bovine("Bovi", "male", birthDate, "Angus", 1, "img.jpg", 10);
            var updateCommand = new UpdateBovineCommand(1, null, "other", null, null, null, null);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => bovine.Update(updateCommand));
        }
    }
}

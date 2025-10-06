using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;

namespace VacApp.Tests.UnitTests
{
    public class CampaignTests
    {
        [Fact]
        public void CreateCampaign_ValidData()
        {
            // Arrange
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var endDate = startDate.AddDays(10);
            var command = new CreateCampaignCommand("Campaña 1", "Descripción", startDate, endDate, 1);

            // Act
            var campaign = new Campaign(command);

            // Assert
            Assert.Equal("Campaña 1", campaign.Name);
            Assert.Equal("Descripción", campaign.Description);
            Assert.Equal(startDate, campaign.StartDate);
            Assert.Equal(endDate, campaign.EndDate);
            Assert.Equal(1, campaign.UserId);
        }
    }
}

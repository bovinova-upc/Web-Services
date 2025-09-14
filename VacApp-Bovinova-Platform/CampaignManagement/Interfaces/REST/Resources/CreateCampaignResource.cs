namespace VacApp_Bovinova_Platform.CampaignManagement.Interfaces.REST.Resources;

public record CreateCampaignResource(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate
    );
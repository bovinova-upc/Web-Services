namespace VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;

public record CreateCampaignCommand(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    int UserId
    );
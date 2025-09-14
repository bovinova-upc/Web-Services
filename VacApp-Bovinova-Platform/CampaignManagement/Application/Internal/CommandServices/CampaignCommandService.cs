using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.CampaignManagement.Application.Internal.CommandServices;

public class CampaignCommandService(ICampaignRepository campaignRepository, IUnitOfWork unitOfWork)
: ICampaignCommandService
{
    public async Task<Campaign?> Handle(CreateCampaignCommand command)
    {
        var campaign = new Campaign(command);
        try
        {
            await campaignRepository.AddAsync(campaign);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception)
        {
            return null;
        }
        return campaign;
    }

    public async Task<IEnumerable<Campaign>> Handle(DeleteCampaignCommand command)
    {
        var campaign = await campaignRepository.FindByIdAsync(command.id);
        if (campaign == null) throw new KeyNotFoundException($"Campaign with id {command.id} not found");

        campaignRepository.Remove(campaign);
        var campaigns = await campaignRepository.ListAsync();
        try
        {
            await unitOfWork.CompleteAsync();
            return campaigns;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
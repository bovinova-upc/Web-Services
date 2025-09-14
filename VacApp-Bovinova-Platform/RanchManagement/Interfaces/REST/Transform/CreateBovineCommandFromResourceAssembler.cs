using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public static class CreateBovineCommandFromResourceAssembler
{
    public static CreateBovineCommand ToCommandFromResource(CreateBovineResource resource, int userId)
    {
        return new CreateBovineCommand(
            resource.Name,
            resource.Gender,
            resource.BirthDate,
            resource.Breed,
            resource.StableId,
            string.Empty, // BovineImg to be set later after file upload
            userId,
            resource.FileData.OpenReadStream()
        );
    }
}
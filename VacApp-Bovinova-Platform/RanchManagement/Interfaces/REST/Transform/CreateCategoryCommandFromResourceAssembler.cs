using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public static class CreateCategoryCommandFromResourceAssembler
{
    public static CreateCategoryCommand ToCommandFromResource(CreateCategoryResource resource, int userId)
    {
        return new CreateCategoryCommand(
            resource.Name,
            userId
        );
    }
}
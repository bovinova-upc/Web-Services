using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

public static class CategoryResourceFromEntityAssembler
{
    public static CategoryResource ToResourceFromEntity(Category entity)
    {
        return new CategoryResource(
            entity.Id,
            entity.Name,
            entity.UserId
        );
    }
}
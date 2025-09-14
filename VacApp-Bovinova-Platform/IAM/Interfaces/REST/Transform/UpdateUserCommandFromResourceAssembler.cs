using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;
using VacApp_Bovinova_Platform.IAM.Interfaces.REST.Resources.UserResources;

namespace VacApp_Bovinova_Platform.IAM.Interfaces.REST.Transform;

public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(UpdateUserResource resource, int id)
    {
        return new UpdateUserCommand(
            id,
            resource.Username,
            resource.Password
        );
    }
}

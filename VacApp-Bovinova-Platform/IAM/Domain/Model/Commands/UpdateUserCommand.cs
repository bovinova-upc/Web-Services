
namespace VacApp_Bovinova_Platform.IAM.Domain.Model.Commands
{
    public record UpdateUserCommand(
        int Id,
        string? Username,
        string? Password
    );
}
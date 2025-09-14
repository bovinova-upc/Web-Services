namespace VacApp_Bovinova_Platform.IAM.Domain.Model.Commands
{
    public record SignUpCommand(
        string Username,
        string Email,
        string Password
    );
}
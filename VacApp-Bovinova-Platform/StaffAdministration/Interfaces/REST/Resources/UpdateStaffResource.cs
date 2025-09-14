namespace VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Resources;

public record UpdateStaffResource(
    string Name,
    int EmployeeStatus
    );
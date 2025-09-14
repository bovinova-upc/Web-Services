using System.ComponentModel.DataAnnotations;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;

namespace VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;

public class Staff
{
    [Required]
    public int Id { get; private set; }

    [Required]
    [StringLength(100)]
    public string Name { get; private set; }

    [Required]
    public EmployeeStatus EmployeeStatus { get; private set; }

    public int UserId { get; set; }

    public Staff()
    {
        Name = "";
        EmployeeStatus = new EmployeeStatus();
    }

    public Staff(string name, int employeeStatus, int userId)
    {
        Name = name;
        EmployeeStatus = new EmployeeStatus(employeeStatus);
        UserId = userId;
    }

    public Staff(CreateStaffCommand command)
    {
        Name = command.Name;
        EmployeeStatus = new EmployeeStatus(command.EmployeeStatus);
        UserId = command.UserId;
    }

    public void Update(UpdateStaffCommand command)
    {
        Name = command.Name;
        EmployeeStatus = new EmployeeStatus(command.EmployeeStatus);
    }
}
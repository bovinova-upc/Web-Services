using VacApp_Bovinova_Platform.Shared.Domain.Repositories;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Repositories;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;

namespace VacApp_Bovinova_Platform.StaffAdministration.Application.Internal.CommandServices;

public class StaffCommandService(IStaffRepository staffRepository,
    IUnitOfWork unitOfWork) : IStaffCommandService
{
    public async Task<Staff?> Handle(CreateStaffCommand command)
    {
        var staff =
            await staffRepository.FindByNameAsync(command.Name);
        if (staff != null)
            throw new Exception($"Staff entity with name '{command.Name}' already exists.");

        staff = new Staff(command);

        try
        {
            await staffRepository.AddAsync(staff);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return staff;
    }

    public async Task<Staff?> Handle(UpdateStaffCommand command)
    {
        var staff = await staffRepository.FindByIdAsync(command.Id);
        if (staff == null)
        {
            throw new Exception($"Staff with ID '{command.Id}' not found.");
        }

        staff.Update(command);

        try
        {
            staffRepository.Update(staff);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return staff;
    }

    public async Task<Staff?> Handle(DeleteStaffCommand command)
    {
        var staff = await staffRepository.FindByIdAsync(command.Id);
        if (staff == null)
            throw new Exception($"Staff with ID '{command.Id}' not found.");

        try
        {
            staffRepository.Remove(staff);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return staff;
    }
}
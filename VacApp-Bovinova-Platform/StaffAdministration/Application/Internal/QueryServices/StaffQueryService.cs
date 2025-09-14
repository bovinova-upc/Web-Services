using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Queries;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Repositories;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;

namespace VacApp_Bovinova_Platform.StaffAdministration.Application.Internal.QueryServices;

public class StaffQueryService(IStaffRepository staffRepository) : IStaffQueryService
{
    public async Task<IEnumerable<Staff>> Handle(GetAllStaffQuery query)
    {
        return await staffRepository.FindByUserIdAsync(query.UserId);
    }

    public async Task<Staff> Handle(GetStaffByIdQuery query)
    {
        var staff = await staffRepository.FindByIdAsync(query.Id);
        if (staff == null) throw new Exception($"Staff with ID '{query.Id}' not found.");
        return staff;
    }

    public async Task<IEnumerable<Staff>> Handle(GetStaffByEmployeeStatusQuery query)
    {
        return await staffRepository.FindByEmployeeStatusAsync(query.EmployeeStatus);
    }

    public async Task<int> CountStaffsByUserIdAsync(int userId)
    {
        var staffs = await staffRepository.FindByUserIdAsync(userId);
        return staffs.Count();
    }
}
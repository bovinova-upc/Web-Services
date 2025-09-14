using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.QueryServices;

public class StableQueryService(IStableRepository stableRepository) : IStableQueryService
{
    public async Task<IEnumerable<Stable>> Handle(GetAllStablesQuery query)
    {
        return await stableRepository.FindByUserIdAsync(query.UserId);
    }

    public async Task<Stable> Handle(GetStablesByIdQuery query)
    {
        return await stableRepository.FindByIdAsync(query.Id);
    }

    public async Task<int> CountStablesByUserIdAsync(int userId)
    {
        var stables = await stableRepository.FindByUserIdAsync(userId);
        return stables.Count();
    }
}
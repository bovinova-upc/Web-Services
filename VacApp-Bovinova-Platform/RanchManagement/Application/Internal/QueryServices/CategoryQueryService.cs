using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.QueryServices;

public class CategoryQueryService(ICategoryRepository categoryRepository) : ICategoryQueryService
{
    public async Task<Category?> Handle(GetCategoryByIdQuery query)
        => await categoryRepository.FindByIdAsync(query.Id);

    public async Task<IEnumerable<Category>> Handle(GetCategoriesByUserIdQuery query)
        => await categoryRepository.FindByUserIdAsync(query.UserId);
}

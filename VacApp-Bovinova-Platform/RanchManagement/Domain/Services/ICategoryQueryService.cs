using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface ICategoryQueryService
{
    Task<Category?> Handle(GetCategoryByIdQuery query);
    Task<IEnumerable<Category>> Handle(GetCategoriesByUserIdQuery query);
}

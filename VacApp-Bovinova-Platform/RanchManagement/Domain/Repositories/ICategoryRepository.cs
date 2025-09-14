using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> FindByIdAsync(int id);
    Task<IEnumerable<Category>> FindByUserIdAsync(int userId);
    Task AddAsync(Category category);
    void Update(Category category);
    void Remove(Category category);
}

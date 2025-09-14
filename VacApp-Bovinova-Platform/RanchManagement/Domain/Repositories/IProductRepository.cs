using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> FindByIdAsync(int id);
    Task<IEnumerable<Product>> FindByUserIdAsync(int userId);
    Task<IEnumerable<Product>> FindByCategoryIdAsync(int categoryId);
    Task AddAsync(Product product);
    void Update(Product product);
    void Remove(Product product);
}

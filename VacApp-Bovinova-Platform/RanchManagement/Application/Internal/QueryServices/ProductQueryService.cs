using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.QueryServices;

public class ProductQueryService(IProductRepository productRepository) : IProductQueryService
{
    public async Task<Product?> Handle(GetProductByIdQuery query)
        => await productRepository.FindByIdAsync(query.Id);

    public async Task<IEnumerable<Product>> Handle(GetProductsByUserIdQuery query)
        => await productRepository.FindByUserIdAsync(query.UserId);

    public async Task<IEnumerable<Product>> Handle(GetProductsByCategoryIdQuery query)
        => await productRepository.FindByCategoryIdAsync(query.CategoryId);

}

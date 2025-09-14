using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Services;

public interface IProductQueryService
{
    Task<Product?> Handle(GetProductByIdQuery query);
    Task<IEnumerable<Product>> Handle(GetProductsByUserIdQuery query);
    Task<IEnumerable<Product>> Handle(GetProductsByCategoryIdQuery query);
}
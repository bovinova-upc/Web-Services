using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace VacApp_Bovinova_Platform.RanchManagement.Infrastructure.Persistence.EFC.Repositories;

public class ProductRepository(AppDbContext ctx) : IProductRepository
{
    public async Task<Product?> FindByIdAsync(int id)
        => await ctx.Products.FindAsync(id);

    public async Task<IEnumerable<Product>> FindByUserIdAsync(int userId)
        => await ctx.Products.Where(p => p.UserId == userId).ToListAsync();

    public async Task<IEnumerable<Product>> FindByCategoryIdAsync(int categoryId)
        => await ctx.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await ctx.Products.ToListAsync();

    public async Task AddAsync(Product product)
        => await ctx.Products.AddAsync(product);

    public void Update(Product product)
        => ctx.Products.Update(product);

    public void Remove(Product product)
        => ctx.Products.Remove(product);
}

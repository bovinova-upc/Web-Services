using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace VacApp_Bovinova_Platform.RanchManagement.Infrastructure.Persistence.EFC.Repositories;

public class CategoryRepository(AppDbContext ctx) : ICategoryRepository
{
    public async Task<Category?> FindByIdAsync(int id)
        => await ctx.Categories.FindAsync(id);

    public async Task<IEnumerable<Category>> FindByUserIdAsync(int userId)
        => await ctx.Categories.Where(c => c.UserId == userId).ToListAsync();

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await ctx.Categories.ToListAsync();

    public async Task AddAsync(Category category)
        => await ctx.Categories.AddAsync(category);

    public void Update(Category category)
        => ctx.Categories.Update(category);

    public void Remove(Category category)
        => ctx.Categories.Remove(category);
}

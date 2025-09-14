using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.CommandServices;

public class CategoryCommandService(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
    ) : ICategoryCommandService
{
    public async Task<Category?> Handle(CreateCategoryCommand command)
    {
        var category = new Category(command);

        try
        {
            await categoryRepository.AddAsync(category);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return category;
    }

    public async Task<Category?> Handle(UpdateCategoryCommand command)
    {
        var category = await categoryRepository.FindByIdAsync(command.Id);
        if (category == null) throw new Exception($"Category with ID '{command.Id}' not found.");

        category.Update(command);

        try
        {
            categoryRepository.Update(category);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


        return category;
    }

    public async Task<Category?> Handle(DeleteCategoryCommand command)
    {
        var category = await categoryRepository.FindByIdAsync(command.Id);
        if (category == null) throw new Exception($"Category with ID '{command.Id}' not found.");

        try
        {
            categoryRepository.Remove(category);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return category;
    }
}

using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.CommandServices;

public class StableCommandService(
    IStableRepository stableRepository,
    IUnitOfWork unitOfWork
    ) : IStableCommandService
{
    public async Task<Stable?> Handle(CreateStableCommand command)
    {
        var stable = new Stable(command);

        try
        {
            await stableRepository.AddAsync(stable);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return stable;
    }

    public async Task<Stable?> Handle(UpdateStableCommand command)
    {
        // Verifies if the stable exists
        var stable = await stableRepository.FindByIdAsync(command.Id);
        if (stable == null)
            throw new Exception($"Stable with ID '{command.Id}' not found.");

        // Updates the stable entity
        stable.Update(command);

        try
        {
            stableRepository.Update(stable);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return stable;
    }

    public async Task<Stable?> Handle(DeleteStableCommand command)
    {
        // Verifies if the stable exists
        var stable = await stableRepository.FindByIdAsync(command.Id);
        if (stable == null)
            throw new Exception($"Stable with ID '{command.Id}' not found.");

        try
        {
            stableRepository.Remove(stable);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return stable;
    }
}
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Application.OutboundServices;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.RanchManagement.Application.Internal.CommandServices;

public class BovineCommandService(
    IBovineRepository bovineRepository,
    IStableRepository stableRepository,
    IMediaStorageService mediaStorageService,
    IUnitOfWork unitOfWork) : IBovineCommandService
{
    public async Task<Bovine?> Handle(CreateBovineCommand command)
    {
        if (command.StableId <= 0)
            throw new Exception("StableId is required.");

        // Verifies if the stable exists
        var stable = await stableRepository.FindByIdAsync(command.StableId);

        if (stable == null)
            throw new Exception($"Stable with ID '{command.StableId}' not found.");

        // Count the current bovines in the stable
        var currentBovineCount = await bovineRepository.CountBovinesByStableIdAsync(command.StableId);
        if (currentBovineCount >= stable.Limit)
        {
            throw new Exception("El establo está lleno. Si quiere añadir más bovinos en este establo deberá incrementar su capacidad máxima.");
        }

        // Creates a new bovine entity
        var bovineImg = mediaStorageService.UploadFileAsync(command.Name, command.FileData);
        var commandWithImg = command with { BovineImg = bovineImg };
        var bovine = new Bovine(commandWithImg);

        try
        {
            await bovineRepository.AddAsync(bovine);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return bovine;
    }

    public async Task<Bovine?> Handle(UpdateBovineCommand command)
    {
        // Verifies if the bovine exists
        var bovine = await bovineRepository.FindByIdAsync(command.Id);
        if (bovine == null)
            throw new Exception($"Bovine with ID '{command.Id}' not found.");

        bovine.Update(command);

        try
        {
            bovineRepository.Update(bovine);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return bovine;
    }

    public async Task<Bovine?> Handle(DeleteBovineCommand command)
    {
        // Verifies if the bovine exists
        var bovine = await bovineRepository.FindByIdAsync(command.Id);
        if (bovine == null)
            throw new Exception($"Bovine with ID '{command.Id}' not found.");

        try
        {
            bovineRepository.Remove(bovine);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return bovine;
    }
}
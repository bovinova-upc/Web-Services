using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;

public class Bovine
{
    [Required]
    public int Id { get; private set; }

    [Required]
    [StringLength(100)]
    public string Name { get; private set; }

    [Required]
    [StringLength(100)]
    public string Gender { get; private set; }

    [Required]
    public DateOnly BirthDate { get; private set; }

    [Required]
    [StringLength(100)]
    public string Breed { get; private set; }

    [Required]
    public int StableId { get; private set; }

    [ForeignKey(nameof(StableId))]
    public Stable? Stable { get; private set; }

    [Required]
    [StringLength(300)]
    public string BovineImg { get; private set; }

    public int UserId { get; set; }

    private Bovine()
    {
        Name = "";
        Gender = "Male";
    }

    public Bovine(string name, string gender, DateOnly birthDate, string breed, int stableId, string bovineImg, int userId)
    {
        Name = name;
        Gender = gender;
        BirthDate = birthDate;
        Breed = breed;
        BovineImg = bovineImg;
        StableId = stableId;
        UserId = userId;
    }

    // Constructor with parameters
    public Bovine(CreateBovineCommand command)
    {
        if (!command.Gender.ToLower().Equals("male") && !command.Gender.ToLower().Equals("female"))
            throw new ArgumentException("Gender must be either 'male' or 'female'");

        Name = command.Name;
        Gender = command.Gender;
        BirthDate = command.BirthDate;
        Breed = command.Breed;
        BovineImg = command.BovineImg;
        StableId = command.StableId;
        UserId = command.UserId;
    }

    //Update Bovine
    public void Update(UpdateBovineCommand command)
    {
        if (command.Name is not null) Name = command.Name;
        if (command.Gender is not null)
        {
            if (!command.Gender.ToLower().Equals("male") && !command.Gender.ToLower().Equals("female"))
                throw new ArgumentException("Gender must be either 'male' or 'female'");
            Gender = command.Gender;
        }
        if (command.BirthDate.HasValue) BirthDate = command.BirthDate.Value;
        if (command.Breed is not null) Breed = command.Breed;
        if (command.StableId.HasValue) StableId = command.StableId.Value;
    }
}
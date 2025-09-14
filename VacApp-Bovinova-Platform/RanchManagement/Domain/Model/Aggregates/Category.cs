using System.ComponentModel.DataAnnotations;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;

namespace VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;

public class Category
{
    [Key]
    public int Id { get; private set; }

    [Required]
    [StringLength(100)]
    public string Name { get; private set; }

    [Required]
    public int UserId { get; private set; }

    // Constructor para EF Core
    private Category() { Name = string.Empty; }

    public Category(string name, int userId)
    {
        Name = name;
        UserId = userId;
    }

    public Category(CreateCategoryCommand command)
    {
        Name = command.Name;
        UserId = command.UserId;
    }

    public void Update(UpdateCategoryCommand command)
    {
        Name = command.Name;
    }
}

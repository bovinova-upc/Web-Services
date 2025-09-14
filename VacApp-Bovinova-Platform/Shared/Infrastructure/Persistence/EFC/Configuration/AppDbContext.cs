using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;

namespace VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* IAM */
        //User
        builder.Entity<User>().HasKey(f => f.Id);
        builder.Entity<User>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(f => f.Username).IsRequired();
        builder.Entity<User>().Property(f => f.Password).IsRequired();
        builder.Entity<User>().Property(f => f.Email).IsRequired();

        /* Ranch Management */
        //Stable
        builder.Entity<Stable>().HasKey(f => f.Id);
        builder.Entity<Stable>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Stable>().Property(f => f.Limit).IsRequired();
        builder.Entity<Stable>().Property(f => f.UserId).HasColumnName("user_id").IsRequired();

        //Bovine
        builder.Entity<Bovine>().HasKey(f => f.Id);
        builder.Entity<Bovine>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Bovine>().Property(f => f.Name).IsRequired();
        builder.Entity<Bovine>().Property(f => f.Gender).IsRequired();
        builder.Entity<Bovine>().Property(f => f.BirthDate).IsRequired();
        builder.Entity<Bovine>().Property(f => f.Breed).IsRequired();
        builder.Entity<Bovine>().Property(f => f.BovineImg).IsRequired();
        builder.Entity<Bovine>().Property(f => f.StableId).IsRequired();
        builder.Entity<Bovine>().Property(f => f.UserId).HasColumnName("user_id").IsRequired();

        // Category
        builder.Entity<Category>().HasKey(f => f.Id);
        builder.Entity<Category>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(f => f.Name).IsRequired();
        builder.Entity<Category>().Property(f => f.UserId).IsRequired();

        // Product
        builder.Entity<Product>().HasKey(f => f.Id);
        builder.Entity<Product>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(f => f.Name).IsRequired();
        builder.Entity<Product>().Property(f => f.CategoryId).IsRequired();
        builder.Entity<Product>().Property(f => f.Quantity).IsRequired();
        builder.Entity<Product>().Property(f => f.UserId).IsRequired();
        builder.Entity<Product>().Property(f => f.ExpirationDate).IsRequired(false);
        builder.Entity<Product>()
            .HasOne(f => f.Category)
            .WithMany()
            .HasForeignKey(f => f.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        /* Staff Administration */
        //Staff
        builder.Entity<Staff>().HasKey(f => f.Id);
        builder.Entity<Staff>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Staff>().Property(f => f.Name).IsRequired();
        builder.Entity<Staff>()
            .OwnsOne(f => f.EmployeeStatus, navigationBuilder =>
            {
                navigationBuilder.WithOwner().HasForeignKey("Id");
                navigationBuilder.Property(f => f.Value)
                    .IsRequired()
                    .HasColumnName("employee_status");
            });
        builder.Entity<Staff>().Property(f => f.UserId).IsRequired().HasColumnName("user_id");

        /* Campaign Management */
        builder.Entity<Campaign>().HasKey(c => c.Id);
        builder.Entity<Campaign>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Campaign>().Property(c => c.Name).IsRequired();
        builder.Entity<Campaign>().Property(c => c.Description).IsRequired();
        builder.Entity<Campaign>().Property(c => c.StartDate).IsRequired();
        builder.Entity<Campaign>().Property(c => c.EndDate).IsRequired();
        builder.Entity<Campaign>().Property(c => c.UserId).IsRequired().HasColumnName("user_id");

        builder.UseSnakeCaseNamingConvention();
    }
}
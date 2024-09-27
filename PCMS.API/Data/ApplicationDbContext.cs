using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Models;
using System.Reflection;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{
    public DbSet<Case> Cases { get; set; }

    public DbSet<CaseAction> CaseActions { get; set; }

    public DbSet<Report> Reports { get; set; }

    public DbSet<Evidence> Evidences { get; set; }

    public DbSet<Person> Persons { get; set; }

    public DbSet<CasePerson> CasePersons { get; set; }

    public DbSet<Location> Locations { get; set; }

    public DbSet<Property> Properties { get; set; }

    public DbSet<ApplicationUserCase> ApplicationUserCases { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<CaseNote> CaseNotes { get; set; }

    public DbSet<CaseEdit> CaseEdits { get; set; }

    public DbSet<CaseTag> CaseTags { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<Charge> Charges { get; set; }

    public DbSet<Release> Releases { get; set; }

    public DbSet<CrimeScene> CrimeScenes { get; set; }

    public DbSet<CrimeSceneCase> CrimeSceneCases { get; set; }

    public DbSet<CrimeScenePerson> CrimeScenePersons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
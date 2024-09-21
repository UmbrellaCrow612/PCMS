using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PCMS.API.Models;
using System.Reflection.Metadata;


// Use fluent API
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // app user

        modelBuilder.Entity<ApplicationUser>().HasKey(x => x.Id);

        modelBuilder.Entity<ApplicationUser>()
               .HasMany(c => c.CreatedCases)
               .WithOne(ca => ca.Creator)
               .HasForeignKey(ca => ca.CreatedById);

        modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.EditedCases)
                .WithOne(ca => ca.LastEditor)
                .HasForeignKey(ca => ca.LastEditedById)
                .IsRequired(false); 

        modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.CreatedCaseActions)
                .WithOne(ca => ca.Creator)
                .HasForeignKey(ca => ca.CreatedById);

        modelBuilder.Entity<ApplicationUser>()
               .HasMany(c => c.EditedCaseActions)
               .WithOne(ca => ca.LastEditor)
               .HasForeignKey(ca => ca.LastEditedById)
               .IsRequired(false);

        modelBuilder.Entity<ApplicationUser>()
             .HasMany(c => c.CreatedReports)
             .WithOne(ca => ca.Creator)
             .HasForeignKey(ca => ca.CreatedById);

        modelBuilder.Entity<ApplicationUser>()
              .HasMany(c => c.EditedReports)
              .WithOne(ca => ca.LastEditor)
              .HasForeignKey(ca => ca.LastEditedById)
              .IsRequired(false);

        // Cases

        modelBuilder.Entity<Case>().HasKey(x => x.Id);

        // Case Actions

        modelBuilder.Entity<CaseAction>().HasKey(x => x.Id);

        modelBuilder.Entity<Case>()
            .HasMany(c => c.CaseActions)
            .WithOne(ca => ca.Case)
            .HasForeignKey(ca => ca.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Reports

        modelBuilder.Entity<Report>().HasKey(x => x.Id);

        modelBuilder.Entity<Case>()
           .HasMany(e => e.Reports)
           .WithOne(e => e.Case)
           .HasForeignKey(e => e.CaseId);

        // Evidences

        modelBuilder.Entity<Evidence>().HasKey(x => x.Id);

        modelBuilder.Entity<Case>()
           .HasMany(e => e.Evidences)
           .WithOne(e => e.Case)
           .HasForeignKey(e => e.CaseId);

        // Persons

        modelBuilder.Entity<Person>().HasKey(x => x.Id);

        // Case Persons

        modelBuilder.Entity<CasePerson>()
            .HasKey(cp => new { cp.CaseId, cp.PersonId });

        modelBuilder.Entity<CasePerson>()
          .HasOne(cp => cp.Case)
          .WithMany(c => c.PersonsInvolved)
          .HasForeignKey(cp => cp.CaseId);

        modelBuilder.Entity<CasePerson>()
            .HasOne(cp => cp.Person)
            .WithMany(p => p.CasesInvolved)
            .HasForeignKey(cp => cp.PersonId);

        // Application User Cases

        modelBuilder.Entity<ApplicationUserCase>()
                .HasKey(uc => new { uc.UserId, uc.CaseId });

        modelBuilder.Entity<ApplicationUserCase>()
            .HasOne(uc => uc.ApplicationUser)
            .WithMany(u => u.AssignedCases)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<ApplicationUserCase>()
                .HasOne(uc => uc.Case)
                .WithMany(c => c.AssignedUsers)
                .HasForeignKey(uc => uc.CaseId);


        // Locations

        modelBuilder.Entity<Location>().HasKey(x => x.Id);

        modelBuilder.Entity<Location>()
           .HasMany(e => e.Properties)
           .WithOne(e => e.Location)
           .HasForeignKey(e => e.LocationId);

        // Properties

        modelBuilder.Entity<Property>().HasKey(x => x.Id);

        // Departments

        modelBuilder.Entity<Department>().HasKey(x => x.Id);

        modelBuilder.Entity<Department>()
             .HasMany(e => e.AssignedUsers)
             .WithOne(e => e.Department)
             .HasForeignKey(e => e.DepartmentId)
             .IsRequired(false);

        modelBuilder.Entity<Department>()
           .HasMany(e => e.AssignedCases)
           .WithOne(e => e.Department)
           .HasForeignKey(e => e.DepartmentId)
           .IsRequired(false);


        // Case Notes

        modelBuilder.Entity<CaseNote>().HasKey(x => x.Id);

        modelBuilder.Entity<Case>()
             .HasMany(e => e.CaseNotes)
             .WithOne(e => e.Case)
             .HasForeignKey(e => e.CaseId);


        // Case edit

        modelBuilder.Entity<CaseEdit>()
          .HasKey(uce => new { uce.UserId, uce.CaseId });

        modelBuilder.Entity<CaseEdit>()
            .HasOne(uce => uce.User)
            .WithMany(u => u.CaseEdits)
            .HasForeignKey(uce => uce.UserId);

        modelBuilder.Entity<CaseEdit>()
            .HasOne(uce => uce.Case)
            .WithMany(c => c.UserEdits)
            .HasForeignKey(uce => uce.CaseId);
    }
}
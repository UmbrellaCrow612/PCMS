using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Models;


// Use fluent API
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{
    public DbSet<Case> Cases { get; set; }

    public DbSet<CaseAction> CaseActions { get; set; }

    public DbSet<Report> Reports { get; set; }

    public DbSet<Evidence> Evidences { get; set; }

    public DbSet<Person> Persons { get; set; }

    public DbSet<CasePerson> CasePersons { get; set; }

    public DbSet<ApplicationUserCase> ApplicationUserCases { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
    }
}
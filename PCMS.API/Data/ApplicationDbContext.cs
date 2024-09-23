using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Models;
using Serilog;
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

    public DbSet<CaseTag> CaseTags { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<Charge> Charges { get; set; }

    public DbSet<Release> Releases { get; set; }

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
            .HasKey(cp => cp.Id);

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


        // Tags

        modelBuilder.Entity<Tag>().HasKey(x => x.Id);

        // Case tags
        modelBuilder.Entity<CaseTag>()
            .HasKey(ct => new { ct.CaseId, ct.TagId });

        modelBuilder.Entity<CaseTag>()
            .HasOne(ct => ct.Case)
            .WithMany(c => c.CaseTags)
            .HasForeignKey(ct => ct.CaseId);

        modelBuilder.Entity<CaseTag>()
            .HasOne(ct => ct.Tag)
            .WithMany(t => t.CaseTags)
            .HasForeignKey(ct => ct.TagId);


        // Bookings

        modelBuilder.Entity<Booking>().HasKey(x => x.Id);

        modelBuilder.Entity<Booking>().HasOne(b => b.User).WithMany(b => b.Bookings).HasForeignKey(b => b.UserId);

        modelBuilder.Entity<Booking>().HasOne(b => b.Person).WithMany(p => p.Bookings).HasForeignKey(b => b.PersonId);

        modelBuilder.Entity<Booking>().HasMany(b => b.Charges).WithOne(c => c.Booking).HasForeignKey(c => c.BookingId);

        modelBuilder.Entity<Booking>().HasOne(b => b.Release).WithOne(r => r.Booking).HasForeignKey<Release>(e => e.BookingId).IsRequired(false);

        modelBuilder.Entity<Booking>().HasOne(b => b.Location).WithMany(l => l.Bookings).HasForeignKey(b => b.LocationId);

        // Charge
        modelBuilder.Entity<Charge>().HasKey(x => x.Id);

        modelBuilder.Entity<Charge>().HasOne(c => c.User).WithMany(u => u.Charges).HasForeignKey(c => c.UserId);

        // Release
        modelBuilder.Entity<Release>().HasKey(x => x.Id);

        modelBuilder.Entity<Release>().HasOne(r => r.User).WithMany(u => u.Releases).HasForeignKey(r => r.UserId);

    }
}
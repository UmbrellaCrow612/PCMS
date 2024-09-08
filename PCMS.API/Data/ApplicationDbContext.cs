using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


// Use fluent API
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{
    public DbSet<Case> Cases { get; set; }

    public DbSet<CaseAction> CaseActions { get; set; }

    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cases

        modelBuilder.Entity<Case>().HasKey(x => x.Id);

        modelBuilder.Entity<ApplicationUser>()
                    .HasMany(e => e.AssignedCases)
                    .WithMany(e => e.AssignedUsers)
                    .UsingEntity("AssignedCasesAssignedUsers");


        modelBuilder.Entity<ApplicationUser>()
                    .HasMany(u => u.CreatedCases)
                    .WithOne(c => c.CreatedBy)
                    .HasForeignKey(c => c.CreatedById);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.ModifiedCases)
            .WithOne(c => c.LastModifiedBy)
            .HasForeignKey(c => c.LastModifiedById);

        // Case Actions

        modelBuilder.Entity<CaseAction>().HasKey(x => x.Id);


        modelBuilder.Entity<Case>()
            .HasMany(e => e.CaseActions)
            .WithOne(e => e.Case)
            .HasForeignKey(e => e.CaseId);

        // Reports

        modelBuilder.Entity<Report>().HasKey(x => x.Id);

        modelBuilder.Entity<Case>()
           .HasMany(e => e.Reports)
           .WithOne(e => e.Case)
           .HasForeignKey(e => e.CaseId);
    }
}
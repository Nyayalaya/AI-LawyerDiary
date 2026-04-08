using CourtApp.Domain.Entities.Masters;
using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

public class IdentityContext : IdentityDbContext<ApplicationUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    // ✅ NEW TABLES
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<UserContact> UserContacts { get; set; }
    public DbSet<ProfessionalInfoEntity> ProfessionalInfos { get; set; }
    public DbSet<UserCourtMapping> UserCourts { get; set; }
    public DbSet<UserHierarchy> UserHierarchies { get; set; }
    public DbSet<UserWorkLocation> UserWorkLocations { get; set; }

    public DbSet<OrganizationEntity> Organizations { get; set; }
    public DbSet<UserOrganizationMapping> UserOrganizations { get; set; }

    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<UserSpecialization> UserSpecializations { get; set; }
    public DbSet<UserBillingModel> UserBillingInfos { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("Identity");

        // 🧑 User Table
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users");

            entity.HasMany(u => u.Addresses)
                  .WithOne(a => a.User)
                  .HasForeignKey(a => a.UserId);

            entity.HasMany(u => u.Contacts)
                  .WithOne(c => c.User)
                  .HasForeignKey(c => c.UserId);

            entity.HasOne(u => u.ProfessionalInfo)
                  .WithOne(p => p.User)
                  .HasForeignKey<ProfessionalInfoEntity>(p => p.UserId);

            entity.HasMany(u => u.UserCourts)
                  .WithOne(uc => uc.User)
                  .HasForeignKey(uc => uc.UserId);
        });

        // 🔗 User Hierarchy (Parent-Child)
        builder.Entity<UserHierarchy>(entity =>
        {
            entity.HasOne(x => x.ParentUser)
                  .WithMany(x => x.Children)
                  .HasForeignKey(x => x.ParentUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.ChildUser)
                  .WithMany(x => x.Parents)
                  .HasForeignKey(x => x.ChildUserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // 🏢 Organization Mapping
        builder.Entity<UserOrganizationMapping>(entity =>
        {
            entity.HasOne(x => x.User)
                  .WithMany(x => x.Organizations)
                  .HasForeignKey(x => x.UserId);

            entity.HasOne(x => x.Organization)
                  .WithMany(x => x.Users)
                  .HasForeignKey(x => x.OrganizationId);
        });

        // ⚖️ User Court Mapping
        builder.Entity<UserCourtMapping>(entity =>
        {
            entity.HasIndex(x => new { x.UserId, x.CourtId, x.CourtHallId })
                  .IsUnique();

            entity.HasOne(x => x.User)
                  .WithMany(x => x.UserCourts)
                  .HasForeignKey(x => x.UserId);
        });

        // 📍 Work Location
        builder.Entity<UserWorkLocation>(entity =>
        {
            entity.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId);
        });

        // 🎯 Specialization Mapping
        builder.Entity<UserSpecialization>(entity =>
        {
            entity.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId);

            entity.HasOne(x => x.Specialization)
                  .WithMany()
                  .HasForeignKey(x => x.SpecializationId);
        });

        // 🔐 Identity Tables Rename
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
    }
}

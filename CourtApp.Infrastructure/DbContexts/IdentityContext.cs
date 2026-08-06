using CourtApp.Domain.Entities;
using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

public class IdentityContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        ChangeTracker.AutoDetectChangesEnabled = false; // 🔥 Performance boost
    }

    #region 🔹 USER EXTENDED TABLES

    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<UserContact> UserContacts { get; set; }
    public DbSet<ProfessionalInfoEntity> ProfessionalInfos { get; set; }
    public DbSet<UserCourtMapping> UserCourts { get; set; }
    public DbSet<UserHierarchy> UserHierarchies { get; set; }
    public DbSet<UserWorkLocation> UserWorkLocations { get; set; }
    public DbSet<UserOrganizationMapping> UserOrganizations { get; set; }
    public DbSet<UserSpecialization> UserSpecializations { get; set; }
    public DbSet<UserBillingModel> UserBillingInfos { get; set; }
    public DbSet<SystemUser> SystemUsers { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ✅ Default Schema
        builder.HasDefaultSchema("public");

        #region 🔹 TABLE RENAMING (CLEAN & CONSISTENT)

        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

        #endregion

        #region 🔹 USER CONFIGURATION

        builder.Entity<ApplicationUser>(entity =>
        {
            // 🔥 Indexing for performance
            entity.HasIndex(x => x.Email).IsUnique();
            entity.HasIndex(x => x.UserName).IsUnique();

            entity.Property(x => x.Email).HasMaxLength(256);
            entity.Property(x => x.UserName).HasMaxLength(256);

            // Relationships
            entity.HasMany(u => u.Addresses)
                  .WithOne(a => a.User)
                  .HasForeignKey(a => a.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.Contacts)
                  .WithOne(c => c.User)
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(u => u.ProfessionalInfo)
                  .WithOne(p => p.User)
                  .HasForeignKey<ProfessionalInfoEntity>(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.UserCourts)
                  .WithOne(uc => uc.User)
                  .HasForeignKey(uc => uc.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        #endregion

        #region 🔹 USER HIERARCHY (CRITICAL MODULE)

        builder.Entity<UserHierarchy>(entity =>
        {
            entity.HasIndex(x => new { x.ParentUserId, x.ChildUserId }).IsUnique();

            entity.HasOne(x => x.ParentUser)
                  .WithMany(x => x.Children)
                  .HasForeignKey(x => x.ParentUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.ChildUser)
                  .WithMany(x => x.Parents)
                  .HasForeignKey(x => x.ChildUserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        #endregion

        #region 🔹 ORGANIZATION MAPPING

        builder.Entity<UserOrganizationMapping>(entity =>
        {
            entity.HasIndex(x => new { x.UserId, x.OrganizationId }).IsUnique();

            entity.HasOne(x => x.User)
                  .WithMany(x => x.Organizations)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Organization)
                  .WithMany(x => x.Users)
                  .HasForeignKey(x => x.OrganizationId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        #endregion

        #region 🔹 USER COURT MAPPING

        builder.Entity<UserCourtMapping>(entity =>
        {
            entity.HasIndex(x => new { x.UserId, x.CourtId, x.CourtHallId })
                  .IsUnique();

            entity.HasOne(x => x.User)
                  .WithMany(x => x.UserCourts)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Note: Court, CourtComplex, CourtHall, and Location are in ApplicationDbContext
            // We only store their IDs here without navigation properties
        });

        #endregion

        #region 🔹 WORK LOCATION

        builder.Entity<UserWorkLocation>(entity =>
        {
            entity.HasIndex(x => new { x.UserId, x.CourtHallId }).IsUnique();

            entity.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Note: Court, CourtComplex, and CourtHall are in ApplicationDbContext
            // We only store their IDs here without navigation properties
        });

        #endregion

        #region 🔹 SPECIALIZATION

        builder.Entity<UserSpecialization>(entity =>
        {
            entity.HasIndex(x => new { x.UserId, x.SpecializationId }).IsUnique();

            entity.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Note: Specialization entity is in ApplicationDbContext
            // We only store its ID here without navigation property
        });

        #endregion

        #region 🔹 BILLING

        builder.Entity<UserBillingModel>(entity =>
        {
            entity.HasIndex(x => x.UserId);

            entity.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        #endregion

        #region 🔹 SYSTEM USERS MANAGEMENT

        builder.Entity<SystemUser>(entity =>
        {
           
            entity.HasIndex(x => x.UserId).IsUnique();
            
            entity.HasIndex(x => x.Status);
            entity.HasIndex(x => x.Subscription);
            entity.HasIndex(x => x.RegisteredDate);
            entity.Property(x => x.StatusReason).HasMaxLength(500);
        });

        #endregion

        #region 🔹 EXTRA PERFORMANCE INDEXES (IMPORTANT)

        builder.Entity<IdentityUserLogin<string>>()
            .HasIndex(x => x.UserId);

        builder.Entity<IdentityUserRole<string>>()
            .HasIndex(x => x.UserId);

        builder.Entity<IdentityUserClaim<string>>()
            .HasIndex(x => x.UserId);

        #endregion
    }
}

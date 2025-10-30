﻿using General.Infrastructure.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace General.Infrastructure.Data;

public class AppDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<SystemPermission> SystemPermissions { get; set; }
    //public DbSet<RolePermission> RolePermissions { get; set; }
    //public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<PermissionCategory> PermissionCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize table names AND define primary keys
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id); // Explicit primary key
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable("Roles");
            entity.HasKey(r => r.Id); // Explicit primary key
        });

        builder.Entity<ApplicationRole>()
        .HasMany(r => r.Permissions)
        .WithMany(p => p.Roles)
        .UsingEntity(j => j.ToTable("RolePermissions"));

        builder.Entity<ApplicationUser>()
        .HasMany(r => r.Permissions)
        .WithMany(p => p.Users)
        .UsingEntity(j => j.ToTable("UserPermissions"));

        // Configuration برای PermissionCategory
        builder.Entity<PermissionCategory>(entity =>
        {
            entity.HasKey(pc => pc.Id);
            entity.HasIndex(pc => pc.Name).IsUnique();

            entity.Property(pc => pc.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(pc => pc.Description)
                .HasMaxLength(200);
        });

        // Configuration برای SystemPermission
        builder.Entity<SystemPermission>(entity =>
        {
            entity.HasKey(sp => sp.Id);
            entity.HasIndex(sp => sp.Name).IsUnique();

            entity.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(sp => sp.Description)
                .HasMaxLength(200);

            entity.HasOne(sp => sp.Category)
                .WithMany(pc => pc.Permissions)
                .HasForeignKey(sp => sp.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
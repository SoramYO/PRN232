using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PRN232.Lab3.CosmeticsStore.Repository.Entities;

public partial class CosmeticsDbContext : DbContext
{
    public CosmeticsDbContext()
    {
    }

    public CosmeticsDbContext(DbContextOptions<CosmeticsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CosmeticCategory> CosmeticCategories { get; set; }

    public virtual DbSet<CosmeticInformation> CosmeticInformations { get; set; }

    public virtual DbSet<SystemAccount> SystemAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database= CosmeticsDB; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CosmeticCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Cosmetic__19093A2B1E1D284D");

            entity.ToTable("CosmeticCategory");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(30)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(120);
            entity.Property(e => e.FormulationType).HasMaxLength(250);
            entity.Property(e => e.UsagePurpose).HasMaxLength(250);
        });

        modelBuilder.Entity<CosmeticInformation>(entity =>
        {
            entity.HasKey(e => e.CosmeticId).HasName("PK__Cosmetic__98ED527E8751E8D7");

            entity.ToTable("CosmeticInformation");

            entity.Property(e => e.CosmeticId)
                .HasMaxLength(30)
                .HasColumnName("CosmeticID");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(30)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CosmeticName).HasMaxLength(160);
            entity.Property(e => e.CosmeticSize).HasMaxLength(400);
            entity.Property(e => e.DollarPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ExpirationDate).HasMaxLength(160);
            entity.Property(e => e.SkinType).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.CosmeticInformations)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__CosmeticI__Categ__3C69FB99");
        });

        modelBuilder.Entity<SystemAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__SystemAc__349DA5864092721B");

            entity.ToTable("SystemAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__SystemAc__49A14740CCFCC9F0").IsUnique();

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.AccountNote).HasMaxLength(240);
            entity.Property(e => e.AccountPassword).HasMaxLength(100);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

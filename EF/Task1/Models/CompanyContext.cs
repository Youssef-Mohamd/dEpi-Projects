using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompanyEFcore.Models;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Depenent> Depenents { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-B0IDBI8\\MSSQLSERVER342;Database=company;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__Departme__0148818EC63E4F97");

            entity.ToTable("Department");

            entity.HasIndex(e => e.ManagerId, "UQ__Departme__3BA2AA8078386679").IsUnique();

            entity.HasIndex(e => e.DeptName, "UQ__Departme__5E50826585008F3D").IsUnique();

            entity.Property(e => e.DeptId)
                .ValueGeneratedNever()
                .HasColumnName("DeptID");
            entity.Property(e => e.DeptName).HasMaxLength(50);
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
        });

        modelBuilder.Entity<Depenent>(entity =>
        {
            entity.HasKey(e => e.DependentId).HasName("PK__DEPENENT__9BC67C117EBD37E8");

            entity.ToTable("DEPENENT");

            entity.Property(e => e.DependentId)
                .ValueGeneratedNever()
                .HasColumnName("DependentID");
            entity.Property(e => e.DependentName).HasMaxLength(50);
            entity.Property(e => e.EmpId).HasColumnName("EmpID");

            entity.HasOne(d => d.Emp).WithMany(p => p.Depenents)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK_Dependent");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBA79CCFE24A7");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId)
                .ValueGeneratedNever()
                .HasColumnName("EmpID");
            entity.Property(e => e.DeptId).HasColumnName("DeptID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.Dept).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Project__761ABED028E092E6");

            entity.ToTable("Project");

            entity.HasIndex(e => e.ProjectName, "UQ__Project__BCBE781CE8CE0CB8").IsUnique();

            entity.Property(e => e.ProjectId)
                .ValueGeneratedNever()
                .HasColumnName("ProjectID");
            entity.Property(e => e.DeptId).HasColumnName("DeptID");
            entity.Property(e => e.ProjectName).HasMaxLength(100);

            entity.HasOne(d => d.Dept).WithMany(p => p.Projects)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Project_Department");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

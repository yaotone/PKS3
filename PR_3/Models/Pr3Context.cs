using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PR_3.Models;

public partial class Pr3Context : DbContext
{
    public Pr3Context()
    {
    }

    public Pr3Context(DbContextOptions<Pr3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Enrollee> Enrollees { get; set; }

    public virtual DbSet<EnrolleeAchievement> EnrolleeAchievements { get; set; }

    public virtual DbSet<EnrolleeSubject> EnrolleeSubjects { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramEnrollee> ProgramEnrollees { get; set; }

    public virtual DbSet<ProgramSubject> ProgramSubjects { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;port=5432;Database=pr3;Username=postgres;Password=9526");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.AchievementId).HasName("achievement_pkey");

            entity.ToTable("achievement");

            entity.Property(e => e.AchievementId)
                .ValueGeneratedNever()
                .HasColumnName("achievement_id");
            entity.Property(e => e.Bonus).HasColumnName("bonus");
            entity.Property(e => e.NameAchievement).HasColumnName("name_achievement");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("department_pkey");

            entity.ToTable("department");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("department_id");
            entity.Property(e => e.NameDepartment).HasColumnName("name_department");
        });

        modelBuilder.Entity<Enrollee>(entity =>
        {
            entity.HasKey(e => e.EnrolleeId).HasName("enrollee_pkey");

            entity.ToTable("enrollee");

            entity.Property(e => e.EnrolleeId)
                .ValueGeneratedNever()
                .HasColumnName("enrollee_id");
            entity.Property(e => e.NameEnrollee).HasColumnName("name_enrollee");
        });

        modelBuilder.Entity<EnrolleeAchievement>(entity =>
        {
            entity.HasKey(e => e.EnrolleeAchievId).HasName("enrollee_achievement_pkey");

            entity.ToTable("enrollee_achievement");

            entity.Property(e => e.EnrolleeAchievId)
                .ValueGeneratedNever()
                .HasColumnName("enrollee_achiev_id");
            entity.Property(e => e.AchievementId).HasColumnName("achievement_id");
            entity.Property(e => e.EnrolleeId).HasColumnName("enrollee_id");

            entity.HasOne(d => d.Achievement).WithMany(p => p.EnrolleeAchievements)
                .HasForeignKey(d => d.AchievementId)
                .HasConstraintName("achievement_id");

            entity.HasOne(d => d.Enrollee).WithMany(p => p.EnrolleeAchievements)
                .HasForeignKey(d => d.EnrolleeId)
                .HasConstraintName("enrollee_id");
        });

        modelBuilder.Entity<EnrolleeSubject>(entity =>
        {
            entity.HasKey(e => e.EnrolleeSubjectId).HasName("enrollee_subject_pkey");

            entity.ToTable("enrollee_subject");

            entity.Property(e => e.EnrolleeSubjectId)
                .ValueGeneratedNever()
                .HasColumnName("enrollee_subject_id");
            entity.Property(e => e.EnrolleeId).HasColumnName("enrollee_id");
            entity.Property(e => e.Result).HasColumnName("result");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Enrollee).WithMany(p => p.EnrolleeSubjects)
                .HasForeignKey(d => d.EnrolleeId)
                .HasConstraintName("enrollee_id");

            entity.HasOne(d => d.Subject).WithMany(p => p.EnrolleeSubjects)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("subject_id");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.ProgramId).HasName("program_pkey");

            entity.ToTable("program");

            entity.Property(e => e.ProgramId)
                .ValueGeneratedNever()
                .HasColumnName("program_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.NameProgram).HasColumnName("name_program");
            entity.Property(e => e.Plan).HasColumnName("plan");

            entity.HasOne(d => d.Department).WithMany(p => p.Programs)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("department_id");
        });

        modelBuilder.Entity<ProgramEnrollee>(entity =>
        {
            entity.HasKey(e => e.ProgramEnrolleeId).HasName("program_enrollee_pkey");

            entity.ToTable("program_enrollee");

            entity.Property(e => e.ProgramEnrolleeId)
                .ValueGeneratedNever()
                .HasColumnName("program_enrollee_id");
            entity.Property(e => e.EnrolleeId).HasColumnName("enrollee_id");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");

            entity.HasOne(d => d.Enrollee).WithMany(p => p.ProgramEnrollees)
                .HasForeignKey(d => d.EnrolleeId)
                .HasConstraintName("enrollee_id");

            entity.HasOne(d => d.Program).WithMany(p => p.ProgramEnrollees)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("program_id");
        });

        modelBuilder.Entity<ProgramSubject>(entity =>
        {
            entity.HasKey(e => e.ProgramSubjectId).HasName("program_subject_pkey");

            entity.ToTable("program_subject");

            entity.Property(e => e.ProgramSubjectId)
                .ValueGeneratedNever()
                .HasColumnName("program_subject_id");
            entity.Property(e => e.MinResult).HasColumnName("min_result");
            entity.Property(e => e.ProgramId).HasColumnName("program_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Program).WithMany(p => p.ProgramSubjects)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("program_id");

            entity.HasOne(d => d.Subject).WithMany(p => p.ProgramSubjects)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("subject_id");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("subject_pkey");

            entity.ToTable("subject");

            entity.Property(e => e.SubjectId)
                .ValueGeneratedNever()
                .HasColumnName("subject_id");
            entity.Property(e => e.NameSubject).HasColumnName("name_subject");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClinicaPet.Models;

public partial class DbClinicaPetContext : DbContext
{
    public DbClinicaPetContext()
    {
    }

    public DbClinicaPetContext(DbContextOptions<DbClinicaPetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agendamento> Agendamentos { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Tutor> Tutor { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConexaoSqlServer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agendamento>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Agendamento");

            entity.Property(e => e.DateHora).HasColumnType("datetime");
            entity.Property(e => e.MotivoConsulta)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CodigoPetsNavigation).WithMany(p => p.Agendamentos)
                .HasForeignKey(d => d.CodigoPets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Agendamento_Pets");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Raca)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.CodigoTutorNavigation).WithMany(p => p.Pets)
                .HasForeignKey(d => d.CodigoTutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pets_Tutor");
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Tutor");

            entity.Property(e => e.Cpf)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CPF");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

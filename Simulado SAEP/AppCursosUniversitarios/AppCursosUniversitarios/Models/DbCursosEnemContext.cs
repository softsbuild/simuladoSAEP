using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppCursosUniversitarios.Models;

public partial class DbCursosEnemContext : DbContext
{
    public DbCursosEnemContext()
    {
    }

    public DbCursosEnemContext(DbContextOptions<DbCursosEnemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Alunos { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<EscolhaCurso> EscolhaCursos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConexaoSqlServer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Aluno");

            entity.Property(e => e.Cpf)
                .HasMaxLength(21)
                .IsUnicode(false)
                .HasColumnName("CPF");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(21)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Curso");

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Turno)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EscolhaCurso>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("EscolhaCurso");

            entity.Property(e => e.DataEscolha).HasColumnType("datetime");

            entity.HasOne(d => d.CodigoAlunoNavigation).WithMany(p => p.EscolhaCursos)
                .HasForeignKey(d => d.CodigoAluno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EscolhaCurso_Aluno");

            entity.HasOne(d => d.CodigoCursoNavigation).WithMany(p => p.EscolhaCursos)
                .HasForeignKey(d => d.CodigoCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EscolhaCurso_Curso");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Usuario");

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Perfil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

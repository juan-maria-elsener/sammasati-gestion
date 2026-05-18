using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Sammasati.App.Models;

namespace Sammasati.App.Data;

public partial class SammasatiDbContext : DbContext
{
    public SammasatiDbContext()
    {
    }

    public SammasatiDbContext(DbContextOptions<SammasatiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Espacio> Espacios { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Modalidade> Modalidades { get; set; }

    public virtual DbSet<PagosCuota> PagosCuotas { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PRIMARY");

            entity.ToTable("alumnos");

            entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fecha_alta");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Observaciones)
                .HasColumnType("text")
                .HasColumnName("observaciones");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.IdClase).HasName("PRIMARY");

            entity.ToTable("clases");

            entity.HasIndex(e => e.IdEspacio, "id_espacio");

            entity.HasIndex(e => e.IdModalidad, "id_modalidad");

            entity.HasIndex(e => e.IdProfesor, "id_profesor");

            entity.Property(e => e.IdClase).HasColumnName("id_clase");
            entity.Property(e => e.CupoMaximo)
                .HasDefaultValueSql("'15'")
                .HasColumnName("cupo_maximo");
            entity.Property(e => e.Dias)
                .HasMaxLength(50)
                .HasColumnName("dias");
            entity.Property(e => e.Horario)
                .HasColumnType("time")
                .HasColumnName("horario");
            entity.Property(e => e.IdEspacio).HasColumnName("id_espacio");
            entity.Property(e => e.IdModalidad).HasColumnName("id_modalidad");
            entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");

            entity.HasOne(d => d.IdEspacioNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdEspacio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clases_ibfk_2");

            entity.HasOne(d => d.IdModalidadNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdModalidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clases_ibfk_3");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdProfesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clases_ibfk_1");
        });

        modelBuilder.Entity<Espacio>(entity =>
        {
            entity.HasKey(e => e.IdEspacio).HasName("PRIMARY");

            entity.ToTable("espacios");

            entity.Property(e => e.IdEspacio).HasColumnName("id_espacio");
            entity.Property(e => e.NombreDireccion)
                .HasMaxLength(100)
                .HasColumnName("nombre_direccion");
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("PRIMARY");

            entity.ToTable("inscripciones");

            entity.HasIndex(e => e.IdAlumno, "id_alumno");

            entity.HasIndex(e => e.IdClase, "id_clase");

            entity.Property(e => e.IdInscripcion).HasColumnName("id_inscripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Activo'")
                .HasColumnName("estado");
            entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");
            entity.Property(e => e.IdClase).HasColumnName("id_clase");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("inscripciones_ibfk_1");

            entity.HasOne(d => d.IdClaseNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdClase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inscripciones_ibfk_2");
        });

        modelBuilder.Entity<Modalidade>(entity =>
        {
            entity.HasKey(e => e.IdModalidad).HasName("PRIMARY");

            entity.ToTable("modalidades");

            entity.Property(e => e.IdModalidad).HasColumnName("id_modalidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<PagosCuota>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PRIMARY");

            entity.ToTable("pagos_cuotas");

            entity.HasIndex(e => e.IdAlumno, "id_alumno");

            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Pendiente'")
                .HasColumnName("estado");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");
            entity.Property(e => e.Mes).HasColumnName("mes");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.PagosCuota)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("pagos_cuotas_ibfk_1");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("PRIMARY");

            entity.ToTable("profesores");

            entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

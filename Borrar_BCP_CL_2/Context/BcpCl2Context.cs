using System;
using System.Collections.Generic;
using Borrar_BCP_CL_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Borrar_BCP_CL_2.Context;

public partial class BcpCl2Context : DbContext
{

    public BcpCl2Context(DbContextOptions<BcpCl2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__CLIENTE__6BEB1D1311F045DE");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.ClienteId).HasColumnName("CLIENTE_ID");
            entity.Property(e => e.Materno)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MATERNO");
            entity.Property(e => e.Nombres)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRES");
            entity.Property(e => e.Paterno)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PATERNO");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.ContratoId).HasName("PK__CONTRATO__5BBA75A7449B0ECF");

            entity.ToTable("CONTRATO");

            entity.Property(e => e.ContratoId).HasColumnName("CONTRATO_ID");
            entity.Property(e => e.Anno)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ANNO");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CIUDAD");
            entity.Property(e => e.ClienteId).HasColumnName("CLIENTE_ID");
            entity.Property(e => e.CodigoContrato)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CODIGO_CONTRATO");
            entity.Property(e => e.Cuenta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CUENTA");
            entity.Property(e => e.DireccionAmbiente)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DIRECCION_AMBIENTE");
            entity.Property(e => e.DocumentoProvedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO_PROVEDOR");
            entity.Property(e => e.Domicilio)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DOMICILIO");
            entity.Property(e => e.FechaFinal).HasColumnName("FECHA_FINAL");
            entity.Property(e => e.FechaFinalArrendamiento).HasColumnName("FECHA_FINAL_ARRENDAMIENTO");
            entity.Property(e => e.FechaInicial).HasColumnName("FECHA_INICIAL");
            entity.Property(e => e.FechaInicialArrendamiento).HasColumnName("FECHA_INICIAL_ARRENDAMIENTO");
            entity.Property(e => e.FechaTenor).HasColumnName("FECHA_TENOR");
            entity.Property(e => e.FechaTestimonio).HasColumnName("FECHA_TESTIMONIO");
            entity.Property(e => e.Importe)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("IMPORTE");
            entity.Property(e => e.Literal)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("LITERAL");
            entity.Property(e => e.MaternoProvedor)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MATERNO_PROVEDOR");
            entity.Property(e => e.Mes)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MES");
            entity.Property(e => e.Meses).HasColumnName("MESES");
            entity.Property(e => e.NombresProvedor)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRES_PROVEDOR");
            entity.Property(e => e.NumeroDireccion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("NUMERO_DIRECCION");
            entity.Property(e => e.NumeroNotaria).HasColumnName("NUMERO_NOTARIA");
            entity.Property(e => e.PaternoProvedor)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PATERNO_PROVEDOR");
            entity.Property(e => e.Superficie).HasColumnName("SUPERFICIE");
            entity.Property(e => e.Testimonio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TESTIMONIO");

            /*entity.HasOne(d => d.Cliente).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__CONTRATO__CLIENT__398D8EEE");*/
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__USUARIOS__9248D09008A314A4");

            entity.ToTable("USUARIOS");

            entity.HasIndex(e => e.Email, "UQ__USUARIOS__161CF724E9D48FF2").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");
            entity.Property(e => e.Contrasenna)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CONTRASENNA");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

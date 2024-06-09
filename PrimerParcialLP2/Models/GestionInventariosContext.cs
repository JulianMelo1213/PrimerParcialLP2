using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PrimerParcialLP2.Models;

public partial class GestionInventariosContext : DbContext
{
    public GestionInventariosContext()
    {
    }

    public GestionInventariosContext(DbContextOptions<GestionInventariosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ajuste> Ajustes { get; set; }

    public virtual DbSet<Almacen> Almacen { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Entradum> Entrada { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Salidum> Salida { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=LAPTOP-TGPNUPFI;Database=GestionInventarios;Trusted_Connection=True;TrustServerCertificate=True;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ajuste>(entity =>
        {
            entity.HasKey(e => e.AjusteId).HasName("PK__Ajuste__AC5997E00010E8C1");

            entity.ToTable("Ajuste");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.Almacen).WithMany(p => p.Ajustes)
                .HasForeignKey(d => d.AlmacenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ajuste__AlmacenI__32E0915F");

            entity.HasOne(d => d.Producto).WithMany(p => p.Ajustes)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ajuste__Producto__31EC6D26");
        });

        modelBuilder.Entity<Almacen>(entity =>
        {
            entity.HasKey(e => e.AlmacenId).HasName("PK__Almacen__022A08768F36B92B");

            entity.ToTable("Almacen");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Ubicacion).HasMaxLength(255);
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1E5C4BF2BD1");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Entradum>(entity =>
        {
            entity.HasKey(e => e.EntradaId).HasName("PK__Entrada__A084667487BC8202");

            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Producto).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrada__Product__2C3393D0");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.InventarioId).HasName("PK__Inventar__FB8A24D7E39A7C7B");

            entity.ToTable("Inventario");

            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Almacen).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.AlmacenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__Almac__36B12243");

            entity.HasOne(d => d.Producto).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__Produ__35BCFE0A");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AEA3756A2187");

            entity.ToTable("Producto");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasMany(d => d.Categoria).WithMany(p => p.Productos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductoCategorium",
                    r => r.HasOne<Categorium>().WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductoC__Categ__3E52440B"),
                    l => l.HasOne<Producto>().WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductoC__Produ__3D5E1FD2"),
                    j =>
                    {
                        j.HasKey("ProductoId", "CategoriaId").HasName("PK__Producto__EB0592BD9CE9FAF7");
                        j.ToTable("ProductoCategoria");
                    });

            entity.HasMany(d => d.Proveedors).WithMany(p => p.Productos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductoProveedor",
                    r => r.HasOne<Proveedor>().WithMany()
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductoP__Prove__3A81B327"),
                    l => l.HasOne<Producto>().WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductoP__Produ__398D8EEE"),
                    j =>
                    {
                        j.HasKey("ProductoId", "ProveedorId").HasName("PK__Producto__2222C806E660A308");
                        j.ToTable("ProductoProveedor");
                    });
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.ProveedorId).HasName("PK__Proveedo__61266A59AE338AB1");

            entity.ToTable("Proveedor");

            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        modelBuilder.Entity<Salidum>(entity =>
        {
            entity.HasKey(e => e.SalidaId).HasName("PK__Salida__DC9971633F2E8E97");

            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Producto).WithMany(p => p.Salida)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Salida__Producto__2F10007B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

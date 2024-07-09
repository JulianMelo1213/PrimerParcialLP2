using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PrimerParcialLP2.Models.Model_Configuration;

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
        => optionsBuilder.UseSqlServer("name=DefaultConnection");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AjusteConfiguration());
        modelBuilder.ApplyConfiguration(new  AlmacenConfiguration());
        modelBuilder.ApplyConfiguration(new  CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new EntradaConfiguration());
        modelBuilder.ApplyConfiguration(new  InventarioConfiguration());
        modelBuilder.ApplyConfiguration(new ProductoConfiguration());
        modelBuilder.ApplyConfiguration(new  ProveedorConfiguration());
        modelBuilder.ApplyConfiguration(new SalidaConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

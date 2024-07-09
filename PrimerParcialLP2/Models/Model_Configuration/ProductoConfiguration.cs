using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrimerParcialLP2.Models.Model_Configuration
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> entity)
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
        }
    }
}

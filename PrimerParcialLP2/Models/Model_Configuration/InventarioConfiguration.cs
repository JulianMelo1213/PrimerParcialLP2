using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrimerParcialLP2.Models.Model_Configuration
{
    public class InventarioConfiguration : IEntityTypeConfiguration<Inventario>
    {
        public void Configure(EntityTypeBuilder<Inventario> entity)
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
          
        }
    }
}

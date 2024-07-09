using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrimerParcialLP2.Models.Model_Configuration
{
    public class AjusteConfiguration : IEntityTypeConfiguration<Ajuste>
    {
        public void Configure(EntityTypeBuilder<Ajuste> entity)
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
        }
    }
}

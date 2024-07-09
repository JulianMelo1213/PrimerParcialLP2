using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrimerParcialLP2.Models.Model_Configuration
{
    public class SalidaConfiguration : IEntityTypeConfiguration<Salidum>
    {
        public void Configure(EntityTypeBuilder<Salidum> entity)
        {

            
                entity.HasKey(e => e.SalidaId).HasName("PK__Salida__DC9971633F2E8E97");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.Producto).WithMany(p => p.Salida)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Salida__Producto__2F10007B");
           
        }
    }
}

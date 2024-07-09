using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrimerParcialLP2.Models.Model_Configuration
{
    public class EntradaConfiguration : IEntityTypeConfiguration<Entradum>
    {
        public void Configure(EntityTypeBuilder<Entradum> entity)
        {
           
                entity.HasKey(e => e.EntradaId).HasName("PK__Entrada__A084667487BC8202");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.Producto).WithMany(p => p.Entrada)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entrada__Product__2C3393D0");
            
        }
    }
}

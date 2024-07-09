using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrimerParcialLP2.Models.Model_Configuration
{
    public class AlmacenConfiguration : IEntityTypeConfiguration<Almacen>
    {
        public void Configure(EntityTypeBuilder<Almacen> entity)
        {
            
                entity.HasKey(e => e.AlmacenId).HasName("PK__Almacen__022A08768F36B92B");

                entity.ToTable("Almacen");

                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Ubicacion).HasMaxLength(255);
            
        }
    }
}

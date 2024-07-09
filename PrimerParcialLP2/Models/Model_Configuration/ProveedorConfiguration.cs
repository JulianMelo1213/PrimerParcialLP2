using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrimerParcialLP2.Models.Model_Configuration
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> entity)
        {
           
                entity.HasKey(e => e.ProveedorId).HasName("PK__Proveedo__61266A59AE338AB1");

                entity.ToTable("Proveedor");

                entity.Property(e => e.Direccion).HasMaxLength(255);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Telefono).HasMaxLength(15);
           
        }
    }
}

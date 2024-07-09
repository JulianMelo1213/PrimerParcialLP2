using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrimerParcialLP2.Models.Model_Configuration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categorium>
    {
        public void Configure(EntityTypeBuilder<Categorium> entity)
        {
           
                entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1E5C4BF2BD1");

                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.Property(e => e.Nombre).HasMaxLength(100);
         
        }
    }
}

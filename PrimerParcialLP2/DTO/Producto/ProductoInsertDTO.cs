using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Producto
{
    public class ProductoInsertDTO
    {

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

    }
}

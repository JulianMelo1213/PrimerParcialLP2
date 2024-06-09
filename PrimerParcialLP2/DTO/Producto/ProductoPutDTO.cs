using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Producto
{
    public class ProductoPutDTO
    {
        public int ProductoId { get; set; }


        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

    }
}

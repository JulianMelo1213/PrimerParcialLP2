

namespace GestionInventarios.Shared.DTOs.Producto
{
    public class ProductoGetDTO
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

    }
}

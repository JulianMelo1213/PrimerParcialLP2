
namespace GestionInventarios.Shared.DTOs.Inventario
{
    public class InventarioGetDTO
    {
        public int InventarioId { get; set; }

        public int ProductoId { get; set; }

        public int AlmacenId { get; set; }

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; }

    }
}

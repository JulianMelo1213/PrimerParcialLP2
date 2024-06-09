using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Inventario
{
    public class InventarioPutDTO
    {
        public int InventarioId { get; set; }

        public int ProductoId { get; set; }

        public int AlmacenId { get; set; }

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; }

    }
}

namespace GestionInventarios.Shared.DTOs.Inventario
{
    public class InventarioGetDTO
    {
        public int InventarioId { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } // Nuevo campo
        public int AlmacenId { get; set; }
        public string AlmacenNombre { get; set; } // Nuevo campo
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}

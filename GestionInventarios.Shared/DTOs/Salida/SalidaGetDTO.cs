namespace GestionInventarios.Shared.DTOs.Salida
{
    public class SalidaGetDTO
    {
        public int SalidaId { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } // Nuevo campo
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}

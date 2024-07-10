

namespace GestionInventarios.Shared.DTOs.Almacen
{
    public class AlmacenGetDTO
    {
        public int AlmacenId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Ubicacion { get; set; }

    }
}

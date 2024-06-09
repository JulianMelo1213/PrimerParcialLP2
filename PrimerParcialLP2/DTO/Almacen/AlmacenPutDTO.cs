using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Almacen
{
    public class AlmacenPutDTO
    {
        public int AlmacenId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Ubicacion { get; set; }

    }
}

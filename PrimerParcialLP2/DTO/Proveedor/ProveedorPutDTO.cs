using PrimerParcialLP2.Models;


namespace PrimerParcialLP2.DTO.Proveedor
{
    public class ProveedorPutDTO
    {
        public int ProveedorId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

    }
}

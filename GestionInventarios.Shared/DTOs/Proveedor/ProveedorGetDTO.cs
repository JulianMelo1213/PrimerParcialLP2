


namespace GestionInventarios.Shared.DTOs.Proveedor
{
    public class ProveedorGetDTO
    {
        public int ProveedorId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

    }
}

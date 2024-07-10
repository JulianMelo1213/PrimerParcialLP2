using System.ComponentModel.DataAnnotations;

namespace GestionInventarios.Shared.DTOs.Proveedor
{
    public class ProveedorInsertDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [StringLength(255, ErrorMessage = "La dirección no puede exceder los 255 caracteres.")]
        public string? Direccion { get; set; }

        [StringLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
        public string? Telefono { get; set; }
    }
}
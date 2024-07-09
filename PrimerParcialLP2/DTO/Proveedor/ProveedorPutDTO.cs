using System.ComponentModel.DataAnnotations;
using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Proveedor
{
    public class ProveedorPutDTO
    {
        [Required(ErrorMessage = "El ProveedorId es obligatorio.")]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [StringLength(255, ErrorMessage = "La dirección no puede exceder los 255 caracteres.")]
        public string? Direccion { get; set; }

        [StringLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
        public string? Telefono { get; set; }
    }
}
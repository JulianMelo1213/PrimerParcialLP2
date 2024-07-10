using System.ComponentModel.DataAnnotations;


namespace GestionInventarios.Shared.DTOs.Almacen
{
    public class AlmacenInsertDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [StringLength(255, ErrorMessage = "La ubicación no puede exceder los 255 caracteres.")]
        public string? Ubicacion { get; set; }
    }
}
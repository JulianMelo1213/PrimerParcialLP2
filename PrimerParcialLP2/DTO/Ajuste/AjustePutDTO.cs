using System;
using System.ComponentModel.DataAnnotations;
using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Ajuste
{
    public class AjustePutDTO
    {
        [Required(ErrorMessage = "El AjusteId es obligatorio.")]
        public int AjusteId { get; set; }

        [Required(ErrorMessage = "El ProductoId es obligatorio.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El AlmacenId es obligatorio.")]
        public int AlmacenId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo no puede exceder los 50 caracteres.")]
        public string Tipo { get; set; } = null!;
    }
}
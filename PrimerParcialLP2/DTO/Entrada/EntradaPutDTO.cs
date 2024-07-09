using System;
using System.ComponentModel.DataAnnotations;
using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Entrada
{
    public class EntradaPutDTO
    {
        [Required(ErrorMessage = "El EntradaId es obligatorio.")]
        public int EntradaId { get; set; }

        [Required(ErrorMessage = "El ProductoId es obligatorio.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }
    }
}
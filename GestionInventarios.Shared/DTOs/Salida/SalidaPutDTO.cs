﻿using System;
using System.ComponentModel.DataAnnotations;


namespace GestionInventarios.Shared.DTOs.Salida
{
    public class SalidaPutDTO
    {
        [Required(ErrorMessage = "El SalidaId es obligatorio.")]
        public int SalidaId { get; set; }

        [Required(ErrorMessage = "El ProductoId es obligatorio.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }
    }
}
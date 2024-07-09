﻿using System.ComponentModel.DataAnnotations;
using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Categoria
{
    public class CategoriaPutDTO
    {
        [Required(ErrorMessage = "El CategoriaId es obligatorio.")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [StringLength(255, ErrorMessage = "La descripción no puede exceder los 255 caracteres.")]
        public string? Descripcion { get; set; }
    }
}
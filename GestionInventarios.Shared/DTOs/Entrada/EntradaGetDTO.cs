﻿namespace GestionInventarios.Shared.DTOs.Entrada
{
    public class EntradaGetDTO
    {
        public int EntradaId { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } // Nuevo campo
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}

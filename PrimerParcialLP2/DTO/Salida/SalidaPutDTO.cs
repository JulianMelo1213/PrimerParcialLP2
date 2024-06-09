﻿using PrimerParcialLP2.Models;


namespace PrimerParcialLP2.DTO.Salida
{
    public class SalidaPutDTO
    {
        public int SalidaId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; }

    }
}

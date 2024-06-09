using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Entrada
{
    public class EntradaGetDTO
    {
        public int EntradaId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; }

    }
}

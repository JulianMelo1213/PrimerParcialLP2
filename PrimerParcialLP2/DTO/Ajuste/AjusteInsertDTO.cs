using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Ajuste
{
    public class AjusteInsertDTO
    {

        public int ProductoId { get; set; }

        public int AlmacenId { get; set; }

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; }

        public string Tipo { get; set; } = null!;
    }
}

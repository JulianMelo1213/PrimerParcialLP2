
namespace GestionInventarios.Shared.DTOs.Ajuste
{
    public class AjusteGetDTO
    {
            public int AjusteId { get; set; }

            public int ProductoId { get; set; }

            public int AlmacenId { get; set; }

            public int Cantidad { get; set; }

            public DateTime Fecha { get; set; }

            public string Tipo { get; set; } = null!;
        
    }
}


namespace GestionInventarios.Shared.DTOs.Categoria
{
    public class CategoriaGetDTO
    {
        public int CategoriaId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

    }
}

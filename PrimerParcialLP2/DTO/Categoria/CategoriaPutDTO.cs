using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO.Categoria
{
    public class CategoriaPutDTO
    {
        public int CategoriaId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

    }
}

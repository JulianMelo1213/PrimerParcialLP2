using GestionInventarios.Shared.DTOs.Categoria;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Client2.Services.Categoria
{
    public interface ICategoriaService
    {
        Task<List<CategoriaGetDTO>> GetCategoriasAsync();
        Task<CategoriaGetDTO> GetCategoriaByIdAsync(int id);
        Task<bool> CreateCategoriaAsync(CategoriaInsertDTO categoria);
        Task<bool> UpdateCategoriaAsync(int id, CategoriaPutDTO categoria);
        Task<bool> DeleteCategoriaAsync(int id);
    }
}

using GestionInventarios.Shared.DTOs.Entrada;
using GestionInventarios.Shared.DTOs.Producto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Client2.Services.Entrada
{
    public interface IEntradaService
    {
        Task<List<EntradaGetDTO>> GetEntradasAsync();
        Task<EntradaGetDTO> GetEntradaByIdAsync(int id);
        Task<bool> CreateEntradaAsync(EntradaInsertDTO entrada);
        Task<bool> UpdateEntradaAsync(int id, EntradaPutDTO entrada);
        Task<bool> DeleteEntradaAsync(int id);

        // Nuevo método
        Task<List<ProductoGetDTO>> GetProductosAsync();
    }
}

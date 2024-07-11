using GestionInventarios.Shared.DTOs.Entrada;
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
    }
}

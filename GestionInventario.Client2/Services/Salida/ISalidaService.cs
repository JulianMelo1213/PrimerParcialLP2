using GestionInventarios.Shared.DTOs.Salida;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Client2.Services.Salida
{
    public interface ISalidaService
    {
        Task<List<SalidaGetDTO>> GetSalidasAsync();
        Task<SalidaGetDTO> GetSalidaByIdAsync(int id);
        Task<bool> CreateSalidaAsync(SalidaInsertDTO salida);
        Task<bool> UpdateSalidaAsync(int id, SalidaPutDTO salida);
        Task<bool> DeleteSalidaAsync(int id);
    }
}

using System.Threading.Tasks;
using System.Collections.Generic;
using PrimerParcialLP2.DTO.Ajuste; 

namespace GestionInventarios.Client.Services
{
    public interface IAjusteService
    {
        Task<List<AjusteGetDTO>> GetAjustesAsync();
        Task<AjusteGetDTO> GetAjusteByIdAsync(int id);
        Task<bool> CreateAjusteAsync(AjusteInsertDTO ajuste);
        Task<bool> UpdateAjusteAsync(int id, AjustePutDTO ajuste);
        Task<bool> DeleteAjusteAsync(int id);
    }
}

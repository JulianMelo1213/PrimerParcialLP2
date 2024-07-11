using GestionInventarios.Shared.DTOs.Almacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Client2.Services.Almacen
{
    public interface IAlmacenService
    {
        Task<List<AlmacenGetDTO>> GetAlmacenesAsync();
        Task<AlmacenGetDTO> GetAlmacenByIdAsync(int id);
        Task<bool> CreateAlmacenAsync(AlmacenInsertDTO almacen);
        Task<bool> UpdateAlmacenAsync(int id, AlmacenPutDTO almacen);
        Task<bool> DeleteAlmacenAsync(int id);
    }
}

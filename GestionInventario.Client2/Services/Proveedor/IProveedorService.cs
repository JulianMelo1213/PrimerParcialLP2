using GestionInventarios.Shared.DTOs.Proveedor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Client2.Services.Proveedor
{
    public interface IProveedorService
    {
        Task<List<ProveedorGetDTO>> GetProveedoresAsync();
        Task<ProveedorGetDTO> GetProveedorByIdAsync(int id);
        Task<bool> CreateProveedorAsync(ProveedorInsertDTO proveedor);
        Task<bool> UpdateProveedorAsync(int id, ProveedorPutDTO proveedor);
        Task<bool> DeleteProveedorAsync(int id);
    }
}

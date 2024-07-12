using GestionInventarios.Shared.DTOs.Inventario;
using GestionInventarios.Shared.DTOs.Producto;
using GestionInventarios.Shared.DTOs.Almacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Client2.Services.Inventario
{
    public interface IInventarioService
    {
        Task<List<InventarioGetDTO>> GetInventariosAsync();
        Task<InventarioGetDTO> GetInventarioByIdAsync(int id);
        Task<bool> CreateInventarioAsync(InventarioInsertDTO inventario);
        Task<bool> UpdateInventarioAsync(int id, InventarioPutDTO inventario);
        Task<bool> DeleteInventarioAsync(int id);

        // Nuevos métodos
        Task<List<ProductoGetDTO>> GetProductosAsync();
        Task<List<AlmacenGetDTO>> GetAlmacenesAsync();
    }
}
